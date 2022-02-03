using System.ComponentModel.DataAnnotations;

namespace dotnet_rpg.Models
{
    public class CharacterClassConfiguration
    {
        public int Id { get; set; }
        [Required]
        public RpgClass Class { get; set; }
        [Range(0, 100)]
        public int MaxStrength { get; set; }
        [Range(0, 100)]
        public int MaxDefence { get; set; }
        [Range(0, 100)]
        public int MaxIntelligence { get; set; }
        [Range(0, 100)]
        public int MaxWeaponDamage { get; set; }
    }
}
