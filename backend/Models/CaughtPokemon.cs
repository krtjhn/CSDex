// Import namespace: System
using System;
// Import namespace: System.ComponentModel.DataAnnotations
using System.ComponentModel.DataAnnotations;
// Import namespace: System.ComponentModel.DataAnnotations.Schema
using System.ComponentModel.DataAnnotations.Schema;
// Import namespace: System.Text.Json.Serialization
using System.Text.Json.Serialization;
// Empty line

// Define namespace: Oopdex.Api.Models
namespace Oopdex.Api.Models
// Start of block scope
{
    // Attribute annotation: [Table("collections")]
    [Table("collections")]
    // Define class CaughtPokemon
    public class CaughtPokemon
    // Start of block scope
    {
        // Attribute annotation: [Key]
        [Key]
        // Execute line: [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Attribute annotation: [Column("id")]
        [Column("id")]
        // Property: Id (Type: long)
        public long Id { get; set; }
// Empty line

        // Attribute annotation: [Column("user_id")]
        [Column("user_id")]
        // Property: UserId (Type: long)
        public long UserId { get; set; }
// Empty line

        // Attribute annotation: [JsonIgnore]
        [JsonIgnore]
        // Attribute annotation: [ForeignKey("UserId")]
        [ForeignKey("UserId")]
        // Property: User (Type: User)
        public virtual User User { get; set; }
// Empty line

        // Attribute annotation: [Column("pokemon_id")]
        [Column("pokemon_id")]
        // Property: PokemonId (Type: int)
        public int PokemonId { get; set; }
// Empty line

        // Attribute annotation: [Column("nickname")]
        [Column("nickname")]
        // Property: Nickname (Type: string?)
        public string? Nickname { get; set; }
// Empty line

        // Attribute annotation: [Column("level")]
        [Column("level")]
        // Property: Level (Type: int?, Default: 1)
        public int? Level { get; set; } = 1;
// Empty line

        // Attribute annotation: [Column("experience")]
        [Column("experience")]
        // Property: Experience (Type: int?, Default: 0)
        public int? Experience { get; set; } = 0;
// Empty line

        // Attribute annotation: [Column("is_favorite")]
        [Column("is_favorite")]
        // Property: IsFavorite (Type: bool?, Default: false)
        public bool? IsFavorite { get; set; } = false;
// Empty line

        // Attribute annotation: [Column("is_active_team_member")]
        [Column("is_active_team_member")]
        // Property: IsActiveTeamMember (Type: bool?, Default: false)
        public bool? IsActiveTeamMember { get; set; } = false;
// Empty line

        // Attribute annotation: [Column("date_caught")]
        [Column("date_caught")]
        // Property: DateCaught (Type: DateTime?, Default: DateTime.Now)
        public DateTime? DateCaught { get; set; } = DateTime.Now;
// Empty line

        // Attribute annotation: [ForeignKey("PokemonId")]
        [ForeignKey("PokemonId")]
        // Property: Pokemon (Type: Pokemon)
        public virtual Pokemon Pokemon { get; set; }
    // End of block scope
    }
// End of block scope
}
