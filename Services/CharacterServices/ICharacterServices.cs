using dotnet_rpg.Models;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterServices;

public interface ICharacterServices
{
    Task<List<Character>> GetAllCharacter();
    Task<Character> GetCharacterById(int id);
    Task<List<Character>> AddCharacter(Character newCharacter);
}
