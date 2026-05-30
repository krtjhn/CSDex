using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oopdex.Api.DTOs;
using Oopdex.Api.Services;

namespace Oopdex.Api.Controllers
{
    [ApiController]
    [Route("api/pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;
        private readonly ICaughtPokemonService _caughtPokemonService;

        public PokemonController(IPokemonService pokemonService, ICaughtPokemonService caughtPokemonService)
        {
            _pokemonService = pokemonService;
            _caughtPokemonService = caughtPokemonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPokemons([FromQuery] int page = 0, [FromQuery] int size = 1000, [FromQuery] string sort = "id", [FromQuery] string direction = "asc")
        {
            var pokemons = await _pokemonService.GetAllPokemonsAsync(page, size, sort, direction);
            return Ok(pokemons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPokemonById(int id)
        {
            var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
            if (pokemon == null) return NotFound(new { error = "Pokemon not found" });
            return Ok(pokemon);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPokemons([FromQuery] string q)
        {
            var pokemons = await _pokemonService.SearchPokemonsAsync(q);
            return Ok(pokemons);
        }

        [HttpGet("my-collection")]
        [Authorize]
        public async Task<IActionResult> GetMyCollection()
        {
            var userIdStr = User.FindFirstValue("userId");
            if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();

            var collection = await _caughtPokemonService.GetCaughtPokemonsByUserIdAsync(userId);
            return Ok(collection);
        }

        [HttpPost("catch")]
        [Authorize]
        public async Task<IActionResult> CatchPokemon([FromBody] CatchPokemonRequest request)
        {
            try
            {
                var userIdStr = User.FindFirstValue("userId");
                if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();

                var caught = await _caughtPokemonService.CatchPokemonAsync(userId, request.PokemonId, request.Nickname);
                return Ok(caught);
            }
            catch (Exception ex) when (ex.Message.Contains("already"))
            {
                return Conflict(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("caught/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCaughtPokemon(long id, [FromBody] UpdateCaughtPokemonRequest request)
        {
            var userIdStr = User.FindFirstValue("userId");
            if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();

            var caught = await _caughtPokemonService.UpdateCaughtPokemonAsync(userId, id, request.Nickname);
            if (caught == null) return NotFound(new { error = "Caught Pokemon not found" });

            return Ok(caught);
        }

        [HttpDelete("release/{id}")]
        [Authorize]
        public async Task<IActionResult> ReleasePokemon(long id)
        {
            var userIdStr = User.FindFirstValue("userId");
            if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();

            var success = await _caughtPokemonService.ReleasePokemonAsync(userId, id);
            if (!success) return NotFound(new { error = "Caught Pokemon not found" });

            return NoContent();
        }

        [HttpDelete("caught/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCaughtPokemon(long id)
        {
            var userIdStr = User.FindFirstValue("userId");
            if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();

            var success = await _caughtPokemonService.ReleasePokemonAsync(userId, id);
            if (!success) return NotFound(new { error = "Caught Pokemon not found" });

            return NoContent();
        }
    }
}
