using dotnet_rpg.Models;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterServices;

public interface ICharacterServices
{
    Task<ServiceResponse<List<Character>>> GetAllCharacter();
    Task<ServiceResponse<Character>> GetCharacterById(int id);
    Task<ServiceResponse<List<Character>>> AddCharacter(Character newCharacter);
}
