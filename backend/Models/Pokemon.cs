using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Oopdex.Api.Models
{
    [Table("pokemons")]
    public class Pokemon
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(80)]
        [Column("name")]
        public string Name { get; set; }

        [Column("height", TypeName = "decimal(10,2)")]
        public decimal? Height { get; set; }

        [Column("weight", TypeName = "decimal(10,2)")]
        public decimal? Weight { get; set; }

        [MaxLength(100)]
        [Column("types")]
        public string? Types { get; set; }

        [MaxLength(255)]
        [Column("abilities")]
        public string? Abilities { get; set; }

        [MaxLength(255)]
        [Column("weaknesses")]
        public string? Weaknesses { get; set; }

        [Range(1, 255)]
        [Column("hp")]
        public int? Hp { get; set; }

        [Range(1, 255)]
        [Column("attack")]
        public int? Attack { get; set; }

        [Range(1, 255)]
        [Column("defense")]
        public int? Defense { get; set; }

        [Range(1, 255)]
        [Column("special_attack")]
        public int? SpecialAttack { get; set; }

        [Range(1, 255)]
        [Column("special_defense")]
        public int? SpecialDefense { get; set; }

        [Range(1, 255)]
        [Column("speed")]
        public int? Speed { get; set; }

        [Column("is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [NotMapped]
        public int PokedexNumber => Id;

        [NotMapped]
        public string? Type1 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Types)) return null;
                var split = Types.Split(',');
                return split.Length > 0 ? split[0].Trim() : null;
            }
        }

        [NotMapped]
        public string? Type2 
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Types)) return null;
                var split = Types.Split(',');
                return split.Length > 1 ? split[1].Trim() : null;
            }
        }

        [NotMapped]
        public string? SpriteUrl => Id > 0 ? $"/assets/pokemon-3d/{Id}.png" : null;

        [NotMapped]
        public string? GifUrl => Id > 0 ? $"/assets/pokemon-gifs/{Id}.gif" : null;
    }
}
