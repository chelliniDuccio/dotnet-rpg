using dotnet_rpg.Models;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterServices
{
    public class CharacterService : ICharacterServices
    {
        private  static List<Character> characters = new List<Character>{
            new Character(),
            new Character{Name = "Duccio"}
        };
        public async Task<List<Character>> GetAllCharacter()
        {
            return characters;
        }

        public async Task<Character> GetCharacterById(int id)
        {
           return characters.FirstOrDefault(c => c.Id == id);
        }
        public async Task<List<Character>> AddCharacter(Character newCharacter)
        {
            characters.Add(newCharacter);
            return characters;
        }
    }
}