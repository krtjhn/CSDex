using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Oopdex.Api.Models
{


    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(20)]
        [Column("username")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [Column("email")]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [Column("password")]
        [JsonIgnore] // Equivalent to write-only for responses
        public string Password { get; set; }

        [Column("role", TypeName = "varchar(50)")]
        public string Role { get; set; } = "ROLE_USER";

        [Column("profile_picture_url")]
        [MaxLength(500)]
        public string? ProfilePictureUrl { get; set; }

        [Column("bio", TypeName = "TEXT")]
        public string? Bio { get; set; }

        [Column("trainer_class")]
        [MaxLength(50)]
        public string? TrainerClass { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [JsonIgnore]
        public virtual ICollection<CaughtPokemon> CaughtPokemons { get; set; } = new List<CaughtPokemon>();
    }
}
