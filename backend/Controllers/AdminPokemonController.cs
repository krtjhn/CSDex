using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Oopdex.Api.Models;
using Oopdex.Api.Services;

namespace Oopdex.Api.Controllers
{
    [ApiController]
    [Route("api/admin/pokemon")]
    [Authorize(Roles = "ROLE_ADMIN")]
    public class AdminPokemonController : ControllerBase
    {
        private readonly IPokemonService _pokemonService;

        public AdminPokemonController(IPokemonService pokemonService)
        {
            _pokemonService = pokemonService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePokemon([FromBody] Pokemon pokemon)
        {
            try
            {
                var created = await _pokemonService.CreatePokemonAsync(pokemon);
                return CreatedAtAction(nameof(PokemonController.GetPokemonById), "Pokemon", new { id = created.Id }, created);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePokemon(int id, [FromBody] Pokemon pokemonUpdate)
        {
            var updated = await _pokemonService.UpdatePokemonAsync(id, pokemonUpdate);
            if (updated == null) return NotFound(new { error = "Pokemon not found" });
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePokemon(int id)
        {
            var success = await _pokemonService.DeletePokemonAsync(id);
            if (!success) return NotFound(new { error = "Pokemon not found" });
            return NoContent();
        }

        [HttpGet("deleted")]
        public async Task<IActionResult> GetDeletedPokemons()
        {
            var pokemons = await _pokemonService.GetDeletedPokemonsAsync();
            return Ok(pokemons);
        }

        [HttpPost("{id}/restore")]
        public async Task<IActionResult> RestorePokemon(int id)
        {
            var success = await _pokemonService.RestorePokemonAsync(id);
            if (!success) return NotFound(new { error = "Pokemon not found" });
            return Ok(new { message = "Pokemon restored successfully" });
        }

        [HttpDelete("{id}/permanent")]
        public async Task<IActionResult> PermanentDeletePokemon(int id)
        {
            var success = await _pokemonService.PermanentDeletePokemonAsync(id);
            if (!success) return NotFound(new { error = "Pokemon not found" });
            return NoContent();
        }
    }
}
