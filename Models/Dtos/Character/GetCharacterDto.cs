using dotnet_rpg.Models;

namespace dotnet_rpg.Models.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoint { get; set; } = 100;
        public int Streigth { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass MyProperty { get; set; } = RpgClass.Knight;
    }
}

