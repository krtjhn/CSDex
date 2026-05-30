using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oopdex.Api.Data;
using Oopdex.Api.Models;

namespace Oopdex.Api.Services
{
    public interface ICaughtPokemonService
    {
        Task<IEnumerable<CaughtPokemon>> GetCaughtPokemonsByUserIdAsync(long userId);
        Task<CaughtPokemon> CatchPokemonAsync(long userId, int pokemonId, string nickname);
        Task<CaughtPokemon> UpdateCaughtPokemonAsync(long userId, long caughtPokemonId, string nickname);
        Task<bool> ReleasePokemonAsync(long userId, long caughtPokemonId);
    }

    public class CaughtPokemonService : ICaughtPokemonService
    {
        private readonly OopdexDbContext _context;

        public CaughtPokemonService(OopdexDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CaughtPokemon>> GetCaughtPokemonsByUserIdAsync(long userId)
        {
            return await _context.CaughtPokemons
                .Include(c => c.Pokemon)
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }

        public async Task<CaughtPokemon> CatchPokemonAsync(long userId, int pokemonId, string nickname)
        {
            if (await _context.CaughtPokemons.AnyAsync(c => c.UserId == userId && c.PokemonId == pokemonId))
                throw new Exception("You have already caught this Pokemon.");

            if (!await _context.Pokemons.AnyAsync(p => p.Id == pokemonId && !p.IsDeleted))
                throw new Exception("Pokemon not found.");

            var caught = new CaughtPokemon
            {
                UserId = userId,
                PokemonId = pokemonId,
                Nickname = nickname,
                Level = 1,
                Experience = 0,
                DateCaught = DateTime.Now
            };

            _context.CaughtPokemons.Add(caught);
            await _context.SaveChangesAsync();

            // Reload with navigation property
            await _context.Entry(caught).Reference(c => c.Pokemon).LoadAsync();
            return caught;
        }

        public async Task<CaughtPokemon> UpdateCaughtPokemonAsync(long userId, long caughtPokemonId, string nickname)
        {
            var caught = await _context.CaughtPokemons.FirstOrDefaultAsync(c => c.Id == caughtPokemonId && c.UserId == userId);
            if (caught == null) return null;

            caught.Nickname = nickname;
            await _context.SaveChangesAsync();
            return caught;
        }

        public async Task<bool> ReleasePokemonAsync(long userId, long caughtPokemonId)
        {
            var caught = await _context.CaughtPokemons.FirstOrDefaultAsync(c => c.Id == caughtPokemonId && c.UserId == userId);
            if (caught == null) return false;

            _context.CaughtPokemons.Remove(caught);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
