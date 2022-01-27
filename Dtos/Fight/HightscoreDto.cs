namespace dotnet_rpg.Dtos.Fight
{
    public class HightscoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Fights { get; set; }
        public int Victory { get; set; }
        public int Defeats { get; set; }
    }
}
