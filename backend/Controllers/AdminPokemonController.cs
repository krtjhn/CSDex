// Import namespace: System.Threading.Tasks
using System.Threading.Tasks;
// Import namespace: Microsoft.AspNetCore.Authorization
using Microsoft.AspNetCore.Authorization;
// Import namespace: Microsoft.AspNetCore.Mvc
using Microsoft.AspNetCore.Mvc;
// Import namespace: Oopdex.Api.Models
using Oopdex.Api.Models;
// Import namespace: Oopdex.Api.Services
using Oopdex.Api.Services;
// Empty line

// Define namespace: Oopdex.Api.Controllers
namespace Oopdex.Api.Controllers
// Start of block scope
{
    // Attribute annotation: [ApiController]
    [ApiController]
    // Execute line: [Route("api/admin/pokemon")]
    [Route("api/admin/pokemon")]
    // Attribute annotation: [Authorize(Roles = "ROLE_ADMIN")]
    [Authorize(Roles = "ROLE_ADMIN")]
    // Define class AdminPokemonController inheriting/implementing ControllerBase
    public class AdminPokemonController : ControllerBase
    // Start of block scope
    {
        // Execute line: private readonly IPokemonService _pokemonService;
        private readonly IPokemonService _pokemonService;
// Empty line

        // Constructor for class: AdminPokemonController (Params: IPokemonService pokemonService)
        public AdminPokemonController(IPokemonService pokemonService)
        // Start of block scope
        {
            // Execute line: _pokemonService = pokemonService;
            _pokemonService = pokemonService;
        // End of block scope
        }
// Empty line

        // Attribute annotation: [HttpPost]
        [HttpPost]
        // Define method: CreatePokemon (Returns: Task<IActionResult>, Params: [FromBody] Pokemon pokemon)
        public async Task<IActionResult> CreatePokemon([FromBody] Pokemon pokemon)
        // Start of block scope
        {
            // Execute line: try
            try
            // Start of block scope
            {
                // Variable declaration and assignment: created = await _pokemonService.CreatePokemonAsync(pokemon)
                var created = await _pokemonService.CreatePokemonAsync(pokemon);
                // Return statement: return CreatedAtAction(nameof(PokemonController.GetPokemonById), "Pokemon", new { id = created.Id }, created);
                return CreatedAtAction(nameof(PokemonController.GetPokemonById), "Pokemon", new { id = created.Id }, created);
            // End of block scope
            }
            // Execute line: catch (System.Exception ex)
            catch (System.Exception ex)
            // Start of block scope
            {
                // Return statement: return BadRequest(new { error = ex.Message });
                return BadRequest(new { error = ex.Message });
            // End of block scope
            }
        // End of block scope
        }
// Empty line

        // Execute line: [HttpPut("{id}")]
        [HttpPut("{id}")]
        // Define method: UpdatePokemon (Returns: Task<IActionResult>, Params: int id, [FromBody] Pokemon pokemonUpdate)
        public async Task<IActionResult> UpdatePokemon(int id, [FromBody] Pokemon pokemonUpdate)
        // Start of block scope
        {
            // Variable declaration and assignment: updated = await _pokemonService.UpdatePokemonAsync(id, pokemonUpdate)
            var updated = await _pokemonService.UpdatePokemonAsync(id, pokemonUpdate);
            // Control Flow: check condition 'if (updated == null) return NotFound(new { error = "Pokemon not found" });'
            if (updated == null) return NotFound(new { error = "Pokemon not found" });
            // Return statement: return Ok(updated);
            return Ok(updated);
        // End of block scope
        }
// Empty line

        // Execute line: [HttpDelete("{id}")]
        [HttpDelete("{id}")]
        // Define method: DeletePokemon (Returns: Task<IActionResult>, Params: int id)
        public async Task<IActionResult> DeletePokemon(int id)
        // Start of block scope
        {
            // Variable declaration and assignment: success = await _pokemonService.DeletePokemonAsync(id)
            var success = await _pokemonService.DeletePokemonAsync(id);
            // Control Flow: check condition 'if (!success) return NotFound(new { error = "Pokemon not found" });'
            if (!success) return NotFound(new { error = "Pokemon not found" });
            // Return statement: return NoContent();
            return NoContent();
        // End of block scope
        }
// Empty line

        // Attribute annotation: [HttpGet("deleted")]
        [HttpGet("deleted")]
        // Define method: GetDeletedPokemons (Returns: Task<IActionResult>, Params: )
        public async Task<IActionResult> GetDeletedPokemons()
        // Start of block scope
        {
            // Variable declaration and assignment: pokemons = await _pokemonService.GetDeletedPokemonsAsync()
            var pokemons = await _pokemonService.GetDeletedPokemonsAsync();
            // Return statement: return Ok(pokemons);
            return Ok(pokemons);
        // End of block scope
        }
// Empty line

        // Execute line: [HttpPost("{id}/restore")]
        [HttpPost("{id}/restore")]
        // Define method: RestorePokemon (Returns: Task<IActionResult>, Params: int id)
        public async Task<IActionResult> RestorePokemon(int id)
        // Start of block scope
        {
            // Variable declaration and assignment: success = await _pokemonService.RestorePokemonAsync(id)
            var success = await _pokemonService.RestorePokemonAsync(id);
            // Control Flow: check condition 'if (!success) return NotFound(new { error = "Pokemon not found" });'
            if (!success) return NotFound(new { error = "Pokemon not found" });
            // Return statement: return Ok(new { message = "Pokemon restored successfully" });
            return Ok(new { message = "Pokemon restored successfully" });
        // End of block scope
        }
// Empty line

        // Execute line: [HttpDelete("{id}/permanent")]
        [HttpDelete("{id}/permanent")]
        // Define method: PermanentDeletePokemon (Returns: Task<IActionResult>, Params: int id)
        public async Task<IActionResult> PermanentDeletePokemon(int id)
        // Start of block scope
        {
            // Variable declaration and assignment: success = await _pokemonService.PermanentDeletePokemonAsync(id)
            var success = await _pokemonService.PermanentDeletePokemonAsync(id);
            // Control Flow: check condition 'if (!success) return NotFound(new { error = "Pokemon not found" });'
            if (!success) return NotFound(new { error = "Pokemon not found" });
            // Return statement: return NoContent();
            return NoContent();
        // End of block scope
        }
    // End of block scope
    }
// End of block scope
}
