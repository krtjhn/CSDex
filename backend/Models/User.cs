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

        [Required(ErrorMessage = "Username is required")]
        [MinLength(3, ErrorMessage = "Username must be between 3 and 20 characters")]
        [MaxLength(20, ErrorMessage = "Username must be between 3 and 20 characters")]
        [Column("username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [Column("email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [Column("password")]
        [JsonIgnore] // Equivalent to write-only for responses
        public string Password { get; set; }

        [Column("role", TypeName = "varchar(50)")]
        public string Role { get; set; } = "ROLE_USER";

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [JsonIgnore]
        public virtual ICollection<CaughtPokemon> CaughtPokemons { get; set; } = new List<CaughtPokemon>();
    }
}
