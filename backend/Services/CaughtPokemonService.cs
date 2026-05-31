// Import namespace: System
using System;
// Import namespace: System.Collections.Generic
using System.Collections.Generic;
// Import namespace: System.Linq
using System.Linq;
// Import namespace: System.Threading.Tasks
using System.Threading.Tasks;
// Import namespace: Microsoft.EntityFrameworkCore
using Microsoft.EntityFrameworkCore;
// Import namespace: Oopdex.Api.Data
using Oopdex.Api.Data;
// Import namespace: Oopdex.Api.Models
using Oopdex.Api.Models;

// Define namespace: Oopdex.Api.Services
namespace Oopdex.Api.Services
// Start of block scope
{
    // Define interface: ICaughtPokemonService
    public interface ICaughtPokemonService
    // Start of block scope
    {
        // Execute line: Task<IEnumerable<CaughtPokemon>> GetCaughtPokemonsByUserI...
        Task<IEnumerable<CaughtPokemon>> GetCaughtPokemonsByUserIdAsync(long userId);
        // Execute line: Task<CaughtPokemon> CatchPokemonAsync(long userId, int po...
        Task<CaughtPokemon> CatchPokemonAsync(long userId, int pokemonId, string nickname);
        // Execute line: Task<CaughtPokemon> UpdateCaughtPokemonAsync(long userId,...
        Task<CaughtPokemon> UpdateCaughtPokemonAsync(long userId, long caughtPokemonId, string nickname);
        // Execute line: Task<bool> ReleasePokemonAsync(long userId, long caughtPo...
        Task<bool> ReleasePokemonAsync(long userId, long caughtPokemonId);
    // End of block scope
    }

    // Define class CaughtPokemonService inheriting/implementing ICaughtPokemonService
    public class CaughtPokemonService : ICaughtPokemonService
    // Start of block scope
    {
        // Execute line: private readonly OopdexDbContext _context;
        private readonly OopdexDbContext _context;

        // Constructor for class: CaughtPokemonService (Params: OopdexDbContext context)
        public CaughtPokemonService(OopdexDbContext context)
        // Start of block scope
        {
            // Execute line: _context = context;
            _context = context;
        // End of block scope
        }

        // Define method: GetCaughtPokemonsByUserIdAsync (Returns: Task<IEnumerable<CaughtPokemon>>, Params: long userId)
        public async Task<IEnumerable<CaughtPokemon>> GetCaughtPokemonsByUserIdAsync(long userId)
        // Start of block scope
        {
            // Return statement: return await _context.CaughtPokemons
            return await _context.CaughtPokemons
                // Execute line: .Include(c => c.Pokemon)
                .Include(c => c.Pokemon)
                // Execute line: .Where(c => c.UserId == userId)
                .Where(c => c.UserId == userId)
                // Execute line: .ToListAsync();
                .ToListAsync();
        // End of block scope
        }

        // Define method: CatchPokemonAsync (Returns: Task<CaughtPokemon>, Params: long userId, int pokemonId, string nickname)
        public async Task<CaughtPokemon> CatchPokemonAsync(long userId, int pokemonId, string nickname)
        // Start of block scope
        {
            // Control Flow: check condition 'if (await _context.CaughtPokemons.AnyAsync(c => c.UserId == userId && c.PokemonId == pokemonId))'
            if (await _context.CaughtPokemons.AnyAsync(c => c.UserId == userId && c.PokemonId == pokemonId))
                // Execute line: throw new Exception("You have already caught this Pokemon...
                throw new Exception("You have already caught this Pokemon.");

            // Control Flow: check condition 'if (!await _context.Pokemons.AnyAsync(p => p.Id == pokemonId && !p.IsDeleted))'
            if (!await _context.Pokemons.AnyAsync(p => p.Id == pokemonId && !p.IsDeleted))
                // Execute line: throw new Exception("Pokemon not found.");
                throw new Exception("Pokemon not found.");

            var caught = new CaughtPokemon
            {
                UserId = userId,
                PokemonId = pokemonId,
                Nickname = nickname,
                DateCaught = DateTime.Now
            };

            // Execute line: _context.CaughtPokemons.Add(caught);
            _context.CaughtPokemons.Add(caught);
            // Execute line: await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();

            // Existing comment: Reload with navigation property
            // Reload with navigation property
            // Execute line: await _context.Entry(caught).Reference(c => c.Pokemon).Lo...
            await _context.Entry(caught).Reference(c => c.Pokemon).LoadAsync();
            // Return statement: return caught;
            return caught;
        // End of block scope
        }

        // Define method: UpdateCaughtPokemonAsync (Returns: Task<CaughtPokemon>, Params: long userId, long caughtPokemonId, string nickname)
        public async Task<CaughtPokemon> UpdateCaughtPokemonAsync(long userId, long caughtPokemonId, string nickname)
        // Start of block scope
        {
            // Variable declaration and assignment: caught = await _context.CaughtPokemons.FirstOrDefaultAsync(c => c.Id == caughtPokemonId && c.UserId == userId)
            var caught = await _context.CaughtPokemons.FirstOrDefaultAsync(c => c.Id == caughtPokemonId && c.UserId == userId);
            // Control Flow: check condition 'if (caught == null) return null;'
            if (caught == null) return null;

            // Execute line: caught.Nickname = nickname;
            caught.Nickname = nickname;
            // Execute line: await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            // Return statement: return caught;
            return caught;
        // End of block scope
        }

        // Define method: ReleasePokemonAsync (Returns: Task<bool>, Params: long userId, long caughtPokemonId)
        public async Task<bool> ReleasePokemonAsync(long userId, long caughtPokemonId)
        // Start of block scope
        {
            // Variable declaration and assignment: caught = await _context.CaughtPokemons.FirstOrDefaultAsync(c => c.Id == caughtPokemonId && c.UserId == userId)
            var caught = await _context.CaughtPokemons.FirstOrDefaultAsync(c => c.Id == caughtPokemonId && c.UserId == userId);
            // Control Flow: check condition 'if (caught == null) return false;'
            if (caught == null) return false;

            // Execute line: _context.CaughtPokemons.Remove(caught);
            _context.CaughtPokemons.Remove(caught);
            // Execute line: await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            // Return statement: return true;
            return true;
        // End of block scope
        }
    // End of block scope
    }
// End of block scope
}
