// Import namespace: System
using System;
// Import namespace: System.Collections.Generic
using System.Collections.Generic;
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
// Empty line

// Empty line

    // Attribute annotation: [Table("users")]
    [Table("users")]
    // Define class User
    public class User
    // Start of block scope
    {
        // Attribute annotation: [Key]
        [Key]
        // Execute line: [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // Property: Id (Type: long)
        public long Id { get; set; }
// Empty line

        // Attribute annotation: [Required]
        [Required]
        // Attribute annotation: [MinLength(3)]
        [MinLength(3)]
        // Attribute annotation: [MaxLength(20)]
        [MaxLength(20)]
        // Attribute annotation: [Column("username")]
        [Column("username")]
        // Property: Username (Type: string)
        public string Username { get; set; }
// Empty line

        // Attribute annotation: [Required]
        [Required]
        // Attribute annotation: [EmailAddress]
        [EmailAddress]
        // Attribute annotation: [Column("email")]
        [Column("email")]
        // Property: Email (Type: string)
        public string Email { get; set; }
// Empty line

        // Attribute annotation: [Required]
        [Required]
        // Attribute annotation: [MinLength(8)]
        [MinLength(8)]
        // Attribute annotation: [Column("password")]
        [Column("password")]
        // Attribute annotation: [JsonIgnore]
        [JsonIgnore] // Equivalent to write-only for responses
        // Property: Password (Type: string)
        public string Password { get; set; }
// Empty line

        // Attribute annotation: [Column("role", TypeName = "varchar(50)")]
        [Column("role", TypeName = "varchar(50)")]
        // Property: Role (Type: string, Default: "ROLE_USER")
        public string Role { get; set; } = "ROLE_USER";
// Empty line

        // Attribute annotation: [Column("profile_picture_url")]
        [Column("profile_picture_url")]
        // Attribute annotation: [MaxLength(500)]
        [MaxLength(500)]
        // Property: ProfilePictureUrl (Type: string?)
        public string? ProfilePictureUrl { get; set; }
// Empty line

        // Attribute annotation: [Column("bio", TypeName = "TEXT")]
        [Column("bio", TypeName = "TEXT")]
        // Property: Bio (Type: string?)
        public string? Bio { get; set; }
// Empty line

        // Attribute annotation: [Column("trainer_class")]
        [Column("trainer_class")]
        // Attribute annotation: [MaxLength(50)]
        [MaxLength(50)]
        // Property: TrainerClass (Type: string?)
        public string? TrainerClass { get; set; }
// Empty line

        // Attribute annotation: [Column("created_at")]
        [Column("created_at")]
        // Property: CreatedAt (Type: DateTime?, Default: DateTime.Now)
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
// Empty line

        // Attribute annotation: [JsonIgnore]
        [JsonIgnore]
        // Property: CaughtPokemons (Type: ICollection<CaughtPokemon>, Default: new List<CaughtPokemon>())
        public virtual ICollection<CaughtPokemon> CaughtPokemons { get; set; } = new List<CaughtPokemon>();
    // End of block scope
    }
// End of block scope
}
