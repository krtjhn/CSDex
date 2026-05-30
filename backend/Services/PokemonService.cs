using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Oopdex.Api.Data;
using Oopdex.Api.Models;

namespace Oopdex.Api.Services
{
    public interface IPokemonService
    {
        Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(int page = 0, int size = 1000, string sort = "id", string direction = "asc");
        Task<Pokemon> GetPokemonByIdAsync(int id);
        Task<Pokemon> CreatePokemonAsync(Pokemon pokemon);
        Task<Pokemon> UpdatePokemonAsync(int id, Pokemon pokemon);
        Task<bool> DeletePokemonAsync(int id);
        Task<IEnumerable<Pokemon>> SearchPokemonsAsync(string query);
        Task<IEnumerable<Pokemon>> GetDeletedPokemonsAsync();
        Task<bool> RestorePokemonAsync(int id);
        Task<bool> PermanentDeletePokemonAsync(int id);
    }

    public class PokemonService : IPokemonService
    {
        private readonly OopdexDbContext _context;

        public PokemonService(OopdexDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(int page = 0, int size = 1000, string sort = "id", string direction = "asc")
        {
            var query = _context.Pokemons.Where(p => !p.IsDeleted);

            query = sort.ToLower() switch
            {
                "name" => direction.ToLower() == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                "hp" => direction.ToLower() == "desc" ? query.OrderByDescending(p => p.Hp) : query.OrderBy(p => p.Hp),
                _ => direction.ToLower() == "desc" ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id)
            };

            return await query.Skip(page * size).Take(size).ToListAsync();
        }

        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        {
            return await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        }

        public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon)
        {
            if (await _context.Pokemons.AnyAsync(p => p.Id == pokemon.Id))
                throw new System.Exception("Pokemon with this ID already exists.");

            _context.Pokemons.Add(pokemon);
            await _context.SaveChangesAsync();
            return pokemon;
        }

        public async Task<Pokemon> UpdatePokemonAsync(int id, Pokemon pokemonUpdate)
        {
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            if (pokemon == null) return null;

            pokemon.Name = pokemonUpdate.Name;
            pokemon.Height = pokemonUpdate.Height;
            pokemon.Weight = pokemonUpdate.Weight;
            pokemon.Types = pokemonUpdate.Types;
            pokemon.Abilities = pokemonUpdate.Abilities;
            pokemon.Weaknesses = pokemonUpdate.Weaknesses;
            pokemon.Hp = pokemonUpdate.Hp;
            pokemon.Attack = pokemonUpdate.Attack;
            pokemon.Defense = pokemonUpdate.Defense;
            pokemon.SpecialAttack = pokemonUpdate.SpecialAttack;
            pokemon.SpecialDefense = pokemonUpdate.SpecialDefense;
            pokemon.Speed = pokemonUpdate.Speed;

            await _context.SaveChangesAsync();
            return pokemon;
        }

        public async Task<bool> DeletePokemonAsync(int id)
        {
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            if (pokemon == null) return false;

            pokemon.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Pokemon>> SearchPokemonsAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return await GetAllPokemonsAsync();

            query = query.ToLower();
            return await _context.Pokemons
                .Where(p => !p.IsDeleted && (p.Name.ToLower().Contains(query) || (p.Types != null && p.Types.ToLower().Contains(query))))
                .ToListAsync();
        }

        public async Task<IEnumerable<Pokemon>> GetDeletedPokemonsAsync()
        {
            return await _context.Pokemons
                .Where(p => p.IsDeleted)
                .OrderBy(p => p.Id)
                .ToListAsync();
        }

        public async Task<bool> RestorePokemonAsync(int id)
        {
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted);
            if (pokemon == null) return false;

            pokemon.IsDeleted = false;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> PermanentDeletePokemonAsync(int id)
        {
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id);
            if (pokemon == null) return false;

            _context.Pokemons.Remove(pokemon);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
