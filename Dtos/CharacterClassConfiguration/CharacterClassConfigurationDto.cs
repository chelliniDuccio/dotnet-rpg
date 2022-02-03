using dotnet_rpg.Models;
using System.ComponentModel.DataAnnotations;

namespace dotnet_rpg.Dtos.CharacterClassConfiguration
{
    public class CharacterClassConfigurationDto
    {
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
