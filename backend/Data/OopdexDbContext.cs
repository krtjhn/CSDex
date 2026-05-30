using Microsoft.EntityFrameworkCore;
using Oopdex.Api.Models;

namespace Oopdex.Api.Data
{
    public class OopdexDbContext : DbContext
    {
        public OopdexDbContext(DbContextOptions<OopdexDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Pokemon> Pokemons { get; set; }
        public DbSet<CaughtPokemon> CaughtPokemons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();
            });

            modelBuilder.Entity<CaughtPokemon>(entity =>
            {
                entity.HasIndex(e => new { e.UserId, e.PokemonId }).IsUnique();
            });
        }
    }
}
