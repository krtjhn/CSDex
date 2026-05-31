using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Oopdex.Api.Models
{
    [Table("collections")]
    public class CaughtPokemon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Column("user_id")]
        public long UserId { get; set; }

        [JsonIgnore]
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [Column("pokemon_id")]
        public int PokemonId { get; set; }

        [Column("nickname")]
        public string? Nickname { get; set; }

        [Column("is_active_team_member")]
        public bool? IsActiveTeamMember { get; set; } = false;

        [Column("date_caught")]
        public DateTime? DateCaught { get; set; } = DateTime.Now;

        [ForeignKey("PokemonId")]
        public virtual Pokemon Pokemon { get; set; }
    }
}
