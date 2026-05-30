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
// Empty line

// Define namespace: Oopdex.Api.Services
namespace Oopdex.Api.Services
// Start of block scope
{
    // Define interface: IPokemonService
    public interface IPokemonService
    // Start of block scope
    {
        // Execute line: Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(int page =...
        Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(int page = 0, int size = 1000, string sort = "id", string direction = "asc");
        // Execute line: Task<Pokemon> GetPokemonByIdAsync(int id);
        Task<Pokemon> GetPokemonByIdAsync(int id);
        // Execute line: Task<Pokemon> CreatePokemonAsync(Pokemon pokemon);
        Task<Pokemon> CreatePokemonAsync(Pokemon pokemon);
        // Execute line: Task<Pokemon> UpdatePokemonAsync(int id, Pokemon pokemon);
        Task<Pokemon> UpdatePokemonAsync(int id, Pokemon pokemon);
        // Execute line: Task<bool> DeletePokemonAsync(int id);
        Task<bool> DeletePokemonAsync(int id);
        // Execute line: Task<IEnumerable<Pokemon>> SearchPokemonsAsync(string que...
        Task<IEnumerable<Pokemon>> SearchPokemonsAsync(string query);
        // Execute line: Task<IEnumerable<Pokemon>> GetDeletedPokemonsAsync();
        Task<IEnumerable<Pokemon>> GetDeletedPokemonsAsync();
        // Execute line: Task<bool> RestorePokemonAsync(int id);
        Task<bool> RestorePokemonAsync(int id);
        // Execute line: Task<bool> PermanentDeletePokemonAsync(int id);
        Task<bool> PermanentDeletePokemonAsync(int id);
    // End of block scope
    }
// Empty line

    // Define class PokemonService inheriting/implementing IPokemonService
    public class PokemonService : IPokemonService
    // Start of block scope
    {
        // Execute line: private readonly OopdexDbContext _context;
        private readonly OopdexDbContext _context;
// Empty line

        // Constructor for class: PokemonService (Params: OopdexDbContext context)
        public PokemonService(OopdexDbContext context)
        // Start of block scope
        {
            // Execute line: _context = context;
            _context = context;
        // End of block scope
        }
// Empty line

        // Define method: GetAllPokemonsAsync (Returns: Task<IEnumerable<Pokemon>>, Params: int page = 0, int size = 1000, string sort = "id", string direction = "asc")
        public async Task<IEnumerable<Pokemon>> GetAllPokemonsAsync(int page = 0, int size = 1000, string sort = "id", string direction = "asc")
        // Start of block scope
        {
            // Variable declaration and assignment: query = _context.Pokemons.Where(p => !p.IsDeleted)
            var query = _context.Pokemons.Where(p => !p.IsDeleted);
// Empty line

            // Execute line: query = sort.ToLower() switch
            query = sort.ToLower() switch
            // Start of block scope
            {
                // Execute line: "name" => direction.ToLower() == "desc" ? query.OrderByDe...
                "name" => direction.ToLower() == "desc" ? query.OrderByDescending(p => p.Name) : query.OrderBy(p => p.Name),
                // Execute line: "hp" => direction.ToLower() == "desc" ? query.OrderByDesc...
                "hp" => direction.ToLower() == "desc" ? query.OrderByDescending(p => p.Hp) : query.OrderBy(p => p.Hp),
                // Execute line: _ => direction.ToLower() == "desc" ? query.OrderByDescend...
                _ => direction.ToLower() == "desc" ? query.OrderByDescending(p => p.Id) : query.OrderBy(p => p.Id)
            // Execute line: };
            };
// Empty line

            // Return statement: return await query.Skip(page * size).Take(size).ToListAsync();
            return await query.Skip(page * size).Take(size).ToListAsync();
        // End of block scope
        }
// Empty line

        // Define method: GetPokemonByIdAsync (Returns: Task<Pokemon>, Params: int id)
        public async Task<Pokemon> GetPokemonByIdAsync(int id)
        // Start of block scope
        {
            // Return statement: return await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            return await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
        // End of block scope
        }
// Empty line

        // Define method: CreatePokemonAsync (Returns: Task<Pokemon>, Params: Pokemon pokemon)
        public async Task<Pokemon> CreatePokemonAsync(Pokemon pokemon)
        // Start of block scope
        {
            // Control Flow: check condition 'if (await _context.Pokemons.AnyAsync(p => p.Id == pokemon.Id))'
            if (await _context.Pokemons.AnyAsync(p => p.Id == pokemon.Id))
                // Execute line: throw new System.Exception("Pokemon with this ID already ...
                throw new System.Exception("Pokemon with this ID already exists.");
// Empty line

            // Execute line: _context.Pokemons.Add(pokemon);
            _context.Pokemons.Add(pokemon);
            // Execute line: await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            // Return statement: return pokemon;
            return pokemon;
        // End of block scope
        }
// Empty line

        // Define method: UpdatePokemonAsync (Returns: Task<Pokemon>, Params: int id, Pokemon pokemonUpdate)
        public async Task<Pokemon> UpdatePokemonAsync(int id, Pokemon pokemonUpdate)
        // Start of block scope
        {
            // Variable declaration and assignment: pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted)
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            // Control Flow: check condition 'if (pokemon == null) return null;'
            if (pokemon == null) return null;
// Empty line

            // Execute line: pokemon.Name = pokemonUpdate.Name;
            pokemon.Name = pokemonUpdate.Name;
            // Execute line: pokemon.Height = pokemonUpdate.Height;
            pokemon.Height = pokemonUpdate.Height;
            // Execute line: pokemon.Weight = pokemonUpdate.Weight;
            pokemon.Weight = pokemonUpdate.Weight;
            // Execute line: pokemon.Types = pokemonUpdate.Types;
            pokemon.Types = pokemonUpdate.Types;
            // Execute line: pokemon.Abilities = pokemonUpdate.Abilities;
            pokemon.Abilities = pokemonUpdate.Abilities;
            // Execute line: pokemon.Weaknesses = pokemonUpdate.Weaknesses;
            pokemon.Weaknesses = pokemonUpdate.Weaknesses;
            // Execute line: pokemon.Hp = pokemonUpdate.Hp;
            pokemon.Hp = pokemonUpdate.Hp;
            // Execute line: pokemon.Attack = pokemonUpdate.Attack;
            pokemon.Attack = pokemonUpdate.Attack;
            // Execute line: pokemon.Defense = pokemonUpdate.Defense;
            pokemon.Defense = pokemonUpdate.Defense;
            // Execute line: pokemon.SpecialAttack = pokemonUpdate.SpecialAttack;
            pokemon.SpecialAttack = pokemonUpdate.SpecialAttack;
            // Execute line: pokemon.SpecialDefense = pokemonUpdate.SpecialDefense;
            pokemon.SpecialDefense = pokemonUpdate.SpecialDefense;
            // Execute line: pokemon.Speed = pokemonUpdate.Speed;
            pokemon.Speed = pokemonUpdate.Speed;
// Empty line

            // Execute line: await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            // Return statement: return pokemon;
            return pokemon;
        // End of block scope
        }
// Empty line

        // Define method: DeletePokemonAsync (Returns: Task<bool>, Params: int id)
        public async Task<bool> DeletePokemonAsync(int id)
        // Start of block scope
        {
            // Variable declaration and assignment: pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted)
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
            // Control Flow: check condition 'if (pokemon == null) return false;'
            if (pokemon == null) return false;
// Empty line

            // Execute line: pokemon.IsDeleted = true;
            pokemon.IsDeleted = true;
            // Execute line: await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            // Return statement: return true;
            return true;
        // End of block scope
        }
// Empty line

        // Define method: SearchPokemonsAsync (Returns: Task<IEnumerable<Pokemon>>, Params: string query)
        public async Task<IEnumerable<Pokemon>> SearchPokemonsAsync(string query)
        // Start of block scope
        {
            // Control Flow: check condition 'if (string.IsNullOrWhiteSpace(query))'
            if (string.IsNullOrWhiteSpace(query))
                // Return statement: return await GetAllPokemonsAsync();
                return await GetAllPokemonsAsync();
// Empty line

            // Execute line: query = query.ToLower();
            query = query.ToLower();
            // Return statement: return await _context.Pokemons
            return await _context.Pokemons
                // Execute line: .Where(p => !p.IsDeleted && (p.Name.ToLower().Contains(qu...
                .Where(p => !p.IsDeleted && (p.Name.ToLower().Contains(query) || (p.Types != null && p.Types.ToLower().Contains(query))))
                // Execute line: .ToListAsync();
                .ToListAsync();
        // End of block scope
        }
// Empty line

        // Define method: GetDeletedPokemonsAsync (Returns: Task<IEnumerable<Pokemon>>, Params: )
        public async Task<IEnumerable<Pokemon>> GetDeletedPokemonsAsync()
        // Start of block scope
        {
            // Return statement: return await _context.Pokemons
            return await _context.Pokemons
                // Execute line: .Where(p => p.IsDeleted)
                .Where(p => p.IsDeleted)
                // Execute line: .OrderBy(p => p.Id)
                .OrderBy(p => p.Id)
                // Execute line: .ToListAsync();
                .ToListAsync();
        // End of block scope
        }
// Empty line

        // Define method: RestorePokemonAsync (Returns: Task<bool>, Params: int id)
        public async Task<bool> RestorePokemonAsync(int id)
        // Start of block scope
        {
            // Variable declaration and assignment: pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted)
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id && p.IsDeleted);
            // Control Flow: check condition 'if (pokemon == null) return false;'
            if (pokemon == null) return false;
// Empty line

            // Execute line: pokemon.IsDeleted = false;
            pokemon.IsDeleted = false;
            // Execute line: await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            // Return statement: return true;
            return true;
        // End of block scope
        }
// Empty line

        // Define method: PermanentDeletePokemonAsync (Returns: Task<bool>, Params: int id)
        public async Task<bool> PermanentDeletePokemonAsync(int id)
        // Start of block scope
        {
            // Variable declaration and assignment: pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id)
            var pokemon = await _context.Pokemons.FirstOrDefaultAsync(p => p.Id == id);
            // Control Flow: check condition 'if (pokemon == null) return false;'
            if (pokemon == null) return false;
// Empty line

            // Execute line: _context.Pokemons.Remove(pokemon);
            _context.Pokemons.Remove(pokemon);
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
