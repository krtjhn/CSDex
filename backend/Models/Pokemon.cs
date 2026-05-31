// Import namespace: System.ComponentModel.DataAnnotations
using System.ComponentModel.DataAnnotations;
// Import namespace: System.ComponentModel.DataAnnotations.Schema
using System.ComponentModel.DataAnnotations.Schema;

// Define namespace: Oopdex.Api.Models
namespace Oopdex.Api.Models
// Start of block scope
{
    // Attribute annotation: [Table("pokemons")]
    [Table("pokemons")]
    // Define class Pokemon
    public class Pokemon
    // Start of block scope
    {
        // Attribute annotation: [Key]
        [Key]
        // Attribute annotation: [Column("id")]
        [Column("id")]
        // Property: Id (Type: int)
        public int Id { get; set; }

        // Attribute annotation: [Required]
        [Required]
        // Attribute annotation: [MaxLength(80)]
        [MaxLength(80)]
        // Attribute annotation: [Column("name")]
        [Column("name")]
        // Property: Name (Type: string)
        public string Name { get; set; }

        // Attribute annotation: [Column("height", TypeName = "decimal(10,2)")]
        [Column("height", TypeName = "decimal(10,2)")]
        // Property: Height (Type: decimal?)
        public decimal? Height { get; set; }

        // Attribute annotation: [Column("weight", TypeName = "decimal(10,2)")]
        [Column("weight", TypeName = "decimal(10,2)")]
        // Property: Weight (Type: decimal?)
        public decimal? Weight { get; set; }

        // Attribute annotation: [MaxLength(100)]
        [MaxLength(100)]
        // Attribute annotation: [Column("types")]
        [Column("types")]
        // Property: Types (Type: string?)
        public string? Types { get; set; }

        // Attribute annotation: [MaxLength(255)]
        [MaxLength(255)]
        // Attribute annotation: [Column("abilities")]
        [Column("abilities")]
        // Property: Abilities (Type: string?)
        public string? Abilities { get; set; }

        // Attribute annotation: [MaxLength(255)]
        [MaxLength(255)]
        // Attribute annotation: [Column("weaknesses")]
        [Column("weaknesses")]
        // Property: Weaknesses (Type: string?)
        public string? Weaknesses { get; set; }

        // Attribute annotation: [Range(1, 255)]
        [Range(1, 255)]
        // Attribute annotation: [Column("hp")]
        [Column("hp")]
        // Property: Hp (Type: int?)
        public int? Hp { get; set; }

        // Attribute annotation: [Range(1, 255)]
        [Range(1, 255)]
        // Attribute annotation: [Column("attack")]
        [Column("attack")]
        // Property: Attack (Type: int?)
        public int? Attack { get; set; }

        // Attribute annotation: [Range(1, 255)]
        [Range(1, 255)]
        // Attribute annotation: [Column("defense")]
        [Column("defense")]
        // Property: Defense (Type: int?)
        public int? Defense { get; set; }

        // Attribute annotation: [Range(1, 255)]
        [Range(1, 255)]
        // Attribute annotation: [Column("special_attack")]
        [Column("special_attack")]
        // Property: SpecialAttack (Type: int?)
        public int? SpecialAttack { get; set; }

        // Attribute annotation: [Range(1, 255)]
        [Range(1, 255)]
        // Attribute annotation: [Column("special_defense")]
        [Column("special_defense")]
        // Property: SpecialDefense (Type: int?)
        public int? SpecialDefense { get; set; }

        // Attribute annotation: [Range(1, 255)]
        [Range(1, 255)]
        // Attribute annotation: [Column("speed")]
        [Column("speed")]
        // Property: Speed (Type: int?)
        public int? Speed { get; set; }

        // Attribute annotation: [Column("is_deleted")]
        [Column("is_deleted")]
        // Property: IsDeleted (Type: bool, Default: false)
        public bool IsDeleted { get; set; } = false;

        // Attribute annotation: [NotMapped]
        [NotMapped]
        // Execute line: public int PokedexNumber => Id;
        public int PokedexNumber => Id;

        // Attribute annotation: [NotMapped]
        [NotMapped]
        // Execute line: public string? Type1
        public string? Type1 
        // Start of block scope
        {
            // Execute line: get
            get
            // Start of block scope
            {
                // Control Flow: check condition 'if (string.IsNullOrWhiteSpace(Types)) return null;'
                if (string.IsNullOrWhiteSpace(Types)) return null;
                // Variable declaration and assignment: split = Types.Split(',')
                var split = Types.Split(',');
                // Return statement: return split.Length > 0 ? split[0].Trim() : null;
                return split.Length > 0 ? split[0].Trim() : null;
            // End of block scope
            }
        // End of block scope
        }

        // Attribute annotation: [NotMapped]
        [NotMapped]
        // Execute line: public string? Type2
        public string? Type2 
        // Start of block scope
        {
            // Execute line: get
            get
            // Start of block scope
            {
                // Control Flow: check condition 'if (string.IsNullOrWhiteSpace(Types)) return null;'
                if (string.IsNullOrWhiteSpace(Types)) return null;
                // Variable declaration and assignment: split = Types.Split(',')
                var split = Types.Split(',');
                // Return statement: return split.Length > 1 ? split[1].Trim() : null;
                return split.Length > 1 ? split[1].Trim() : null;
            // End of block scope
            }
        // End of block scope
        }

        // Attribute annotation: [NotMapped]
        [NotMapped]
        // Execute line: public string? SpriteUrl => Id > 0 ? $"/assets/pokemon-3d...
        public string? SpriteUrl => Id > 0 ? $"/assets/pokemon-3d/{Id}.png" : null;

        // Attribute annotation: [NotMapped]
        [NotMapped]
        // Execute line: public string? GifUrl => Id > 0 ? $"/assets/pokemon-gifs/...
        public string? GifUrl => Id > 0 ? $"/assets/pokemon-gifs/{Id}.gif" : null;
    // End of block scope
    }
// End of block scope
}
