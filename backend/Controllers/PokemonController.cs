// Import namespace: System
using System;
// Import namespace: System.Security.Claims
using System.Security.Claims;
// Import namespace: System.Threading.Tasks
using System.Threading.Tasks;
// Import namespace: Microsoft.AspNetCore.Authorization
using Microsoft.AspNetCore.Authorization;
// Import namespace: Microsoft.AspNetCore.Mvc
using Microsoft.AspNetCore.Mvc;
// Import namespace: Oopdex.Api.DTOs
using Oopdex.Api.DTOs;
// Import namespace: Oopdex.Api.Services
using Oopdex.Api.Services;

// Define namespace: Oopdex.Api.Controllers
namespace Oopdex.Api.Controllers
// Start of block scope
{
    // Attribute annotation: [ApiController]
    [ApiController]
    // Execute line: [Route("api/pokemon")]
    [Route("api/pokemon")]
    // Define class PokemonController inheriting/implementing ControllerBase
    public class PokemonController : ControllerBase
    // Start of block scope
    {
        // Execute line: private readonly IPokemonService _pokemonService;
        private readonly IPokemonService _pokemonService;
        // Execute line: private readonly ICaughtPokemonService _caughtPokemonServ...
        private readonly ICaughtPokemonService _caughtPokemonService;

        // Constructor for class: PokemonController (Params: IPokemonService pokemonService, ICaughtPokemonService caughtPokemonService)
        public PokemonController(IPokemonService pokemonService, ICaughtPokemonService caughtPokemonService)
        // Start of block scope
        {
            // Execute line: _pokemonService = pokemonService;
            _pokemonService = pokemonService;
            // Execute line: _caughtPokemonService = caughtPokemonService;
            _caughtPokemonService = caughtPokemonService;
        // End of block scope
        }

        // Attribute annotation: [HttpGet]
        [HttpGet]
        // Define method: GetAllPokemons (Returns: Task<IActionResult>, Params: [FromQuery] int page = 0, [FromQuery] int size = 1000, [FromQuery] string sort = "id", [FromQuery] string direction = "asc")
        public async Task<IActionResult> GetAllPokemons([FromQuery] int page = 0, [FromQuery] int size = 1000, [FromQuery] string sort = "id", [FromQuery] string direction = "asc")
        // Start of block scope
        {
            // Variable declaration and assignment: pokemons = await _pokemonService.GetAllPokemonsAsync(page, size, sort, direction)
            var pokemons = await _pokemonService.GetAllPokemonsAsync(page, size, sort, direction);
            // Return statement: return Ok(pokemons);
            return Ok(pokemons);
        // End of block scope
        }

        // Execute line: [HttpGet("{id}")]
        [HttpGet("{id}")]
        // Define method: GetPokemonById (Returns: Task<IActionResult>, Params: int id)
        public async Task<IActionResult> GetPokemonById(int id)
        // Start of block scope
        {
            // Variable declaration and assignment: pokemon = await _pokemonService.GetPokemonByIdAsync(id)
            var pokemon = await _pokemonService.GetPokemonByIdAsync(id);
            // Control Flow: check condition 'if (pokemon == null) return NotFound(new { error = "Pokemon not found" });'
            if (pokemon == null) return NotFound(new { error = "Pokemon not found" });
            // Return statement: return Ok(pokemon);
            return Ok(pokemon);
        // End of block scope
        }

        // Attribute annotation: [HttpGet("search")]
        [HttpGet("search")]
        // Define method: SearchPokemons (Returns: Task<IActionResult>, Params: [FromQuery] string q)
        public async Task<IActionResult> SearchPokemons([FromQuery] string q)
        // Start of block scope
        {
            // Variable declaration and assignment: pokemons = await _pokemonService.SearchPokemonsAsync(q)
            var pokemons = await _pokemonService.SearchPokemonsAsync(q);
            // Return statement: return Ok(pokemons);
            return Ok(pokemons);
        // End of block scope
        }

        // Execute line: [HttpGet("my-collection")]
        [HttpGet("my-collection")]
        // Attribute annotation: [Authorize]
        [Authorize]
        // Define method: GetMyCollection (Returns: Task<IActionResult>, Params: )
        public async Task<IActionResult> GetMyCollection()
        // Start of block scope
        {
            // Variable declaration and assignment: userIdStr = User.FindFirstValue("userId")
            var userIdStr = User.FindFirstValue("userId");
            // Control Flow: check condition 'if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();'
            if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();

            // Variable declaration and assignment: collection = await _caughtPokemonService.GetCaughtPokemonsByUserIdAsync(userId)
            var collection = await _caughtPokemonService.GetCaughtPokemonsByUserIdAsync(userId);
            // Return statement: return Ok(collection);
            return Ok(collection);
        // End of block scope
        }

        // Attribute annotation: [HttpPost("catch")]
        [HttpPost("catch")]
        // Attribute annotation: [Authorize]
        [Authorize]
        // Define method: CatchPokemon (Returns: Task<IActionResult>, Params: [FromBody] CatchPokemonRequest request)
        public async Task<IActionResult> CatchPokemon([FromBody] CatchPokemonRequest request)
        // Start of block scope
        {
            // Execute line: try
            try
            // Start of block scope
            {
                // Variable declaration and assignment: userIdStr = User.FindFirstValue("userId")
                var userIdStr = User.FindFirstValue("userId");
                // Control Flow: check condition 'if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();'
                if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();

                // Variable declaration and assignment: caught = await _caughtPokemonService.CatchPokemonAsync(userId, request.PokemonId, request.Nickname)
                var caught = await _caughtPokemonService.CatchPokemonAsync(userId, request.PokemonId, request.Nickname);
                // Return statement: return Ok(caught);
                return Ok(caught);
            // End of block scope
            }
            // Execute line: catch (Exception ex) when (ex.Message.Contains("already"))
            catch (Exception ex) when (ex.Message.Contains("already"))
            // Start of block scope
            {
                // Return statement: return Conflict(new { error = ex.Message });
                return Conflict(new { error = ex.Message });
            // End of block scope
            }
            // Execute line: catch (Exception ex)
            catch (Exception ex)
            // Start of block scope
            {
                // Return statement: return BadRequest(new { error = ex.Message });
                return BadRequest(new { error = ex.Message });
            // End of block scope
            }
        // End of block scope
        }

        // Execute line: [HttpPut("caught/{id}")]
        [HttpPut("caught/{id}")]
        // Attribute annotation: [Authorize]
        [Authorize]
        // Define method: UpdateCaughtPokemon (Returns: Task<IActionResult>, Params: long id, [FromBody] UpdateCaughtPokemonRequest request)
        public async Task<IActionResult> UpdateCaughtPokemon(long id, [FromBody] UpdateCaughtPokemonRequest request)
        // Start of block scope
        {
            // Variable declaration and assignment: userIdStr = User.FindFirstValue("userId")
            var userIdStr = User.FindFirstValue("userId");
            // Control Flow: check condition 'if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();'
            if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();

            // Variable declaration and assignment: caught = await _caughtPokemonService.UpdateCaughtPokemonAsync(userId, id, request.Nickname)
            var caught = await _caughtPokemonService.UpdateCaughtPokemonAsync(userId, id, request.Nickname);
            // Control Flow: check condition 'if (caught == null) return NotFound(new { error = "Caught Pokemon not found" });'
            if (caught == null) return NotFound(new { error = "Caught Pokemon not found" });

            // Return statement: return Ok(caught);
            return Ok(caught);
        // End of block scope
        }

        // Execute line: [HttpDelete("release/{id}")]
        [HttpDelete("release/{id}")]
        // Attribute annotation: [Authorize]
        [Authorize]
        // Define method: ReleasePokemon (Returns: Task<IActionResult>, Params: long id)
        public async Task<IActionResult> ReleasePokemon(long id)
        // Start of block scope
        {
            // Variable declaration and assignment: userIdStr = User.FindFirstValue("userId")
            var userIdStr = User.FindFirstValue("userId");
            // Control Flow: check condition 'if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();'
            if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();

            // Variable declaration and assignment: success = await _caughtPokemonService.ReleasePokemonAsync(userId, id)
            var success = await _caughtPokemonService.ReleasePokemonAsync(userId, id);
            // Control Flow: check condition 'if (!success) return NotFound(new { error = "Caught Pokemon not found" });'
            if (!success) return NotFound(new { error = "Caught Pokemon not found" });

            // Return statement: return NoContent();
            return NoContent();
        // End of block scope
        }

        // Execute line: [HttpDelete("caught/{id}")]
        [HttpDelete("caught/{id}")]
        // Attribute annotation: [Authorize]
        [Authorize]
        // Define method: DeleteCaughtPokemon (Returns: Task<IActionResult>, Params: long id)
        public async Task<IActionResult> DeleteCaughtPokemon(long id)
        // Start of block scope
        {
            // Variable declaration and assignment: userIdStr = User.FindFirstValue("userId")
            var userIdStr = User.FindFirstValue("userId");
            // Control Flow: check condition 'if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();'
            if (!long.TryParse(userIdStr, out var userId)) return Unauthorized();

            // Variable declaration and assignment: success = await _caughtPokemonService.ReleasePokemonAsync(userId, id)
            var success = await _caughtPokemonService.ReleasePokemonAsync(userId, id);
            // Control Flow: check condition 'if (!success) return NotFound(new { error = "Caught Pokemon not found" });'
            if (!success) return NotFound(new { error = "Caught Pokemon not found" });

            // Return statement: return NoContent();
            return NoContent();
        // End of block scope
        }
    // End of block scope
    }
// End of block scope
}
