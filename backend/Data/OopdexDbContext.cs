// Import namespace: Microsoft.EntityFrameworkCore
using Microsoft.EntityFrameworkCore;
// Import namespace: Oopdex.Api.Models
using Oopdex.Api.Models;

// Define namespace: Oopdex.Api.Data
namespace Oopdex.Api.Data
// Start of block scope
{
    // Define class OopdexDbContext inheriting/implementing DbContext
    public class OopdexDbContext : DbContext
    // Start of block scope
    {
        // Constructor for class: OopdexDbContext (Params: DbContextOptions<OopdexDbContext> options) : base(options)
        public OopdexDbContext(DbContextOptions<OopdexDbContext> options) : base(options)
        // Start of block scope
        {
        // End of block scope
        }

        // Property: Users (Type: DbSet<User>)
        public DbSet<User> Users { get; set; }
        // Property: Pokemons (Type: DbSet<Pokemon>)
        public DbSet<Pokemon> Pokemons { get; set; }
        // Property: CaughtPokemons (Type: DbSet<CaughtPokemon>)
        public DbSet<CaughtPokemon> CaughtPokemons { get; set; }

        // Define method: OnModelCreating (Returns: void, Params: ModelBuilder modelBuilder)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        // Start of block scope
        {
            // Execute line: base.OnModelCreating(modelBuilder);
            base.OnModelCreating(modelBuilder);

            // Execute line: modelBuilder.Entity<User>(entity =>
            modelBuilder.Entity<User>(entity =>
            // Start of block scope
            {
                // Execute line: entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Username).IsUnique();
                // Execute line: entity.HasIndex(e => e.Email).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            // Execute line: });
            });

            // Execute line: modelBuilder.Entity<CaughtPokemon>(entity =>
            modelBuilder.Entity<CaughtPokemon>(entity =>
            // Start of block scope
            {
                // Execute line: entity.HasIndex(e => new { e.UserId, e.PokemonId }).IsUni...
                entity.HasIndex(e => new { e.UserId, e.PokemonId }).IsUnique();
            // Execute line: });
            });
        // End of block scope
        }
    // End of block scope
    }
// End of block scope
}
