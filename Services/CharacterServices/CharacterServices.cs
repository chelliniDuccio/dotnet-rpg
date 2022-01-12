using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterServices
{
    public class CharacterService : ICharacterServices
    {
        private  static List<Character> characters = new List<Character>{
            new Character(),
            new Character{Name = "Duccio"}
        };
        public List<Character> GetAllCharacter()
        {
            return characters;
        }

        public Character GetCharacterById(int id)
        {
           return characters.FirstOrDefault(c => c.Id == id);
        }
        public List<Character> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }
    }
}