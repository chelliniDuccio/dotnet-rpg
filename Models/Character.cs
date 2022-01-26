using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace dotnet_rpg.Models
{
    public class Character
    {
        public int  Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoint {get; set;} = 100;
        public int Streigth { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public User User { get; set; }
        public Weapon Weapon { get; set; }
        public List<Skill> Skills { get; set; }
        public int Fights { get; set; }
        public int Victory { get; set; }
        public int Defeats { get; set; }
    }
}