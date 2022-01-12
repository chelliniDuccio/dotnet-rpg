using dotnet_rpg.Models;
using dotnet_rpg.Models.Dtos.Character;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterServices;

public interface ICharacterServices
{
    Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacter();
    Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
    Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
}
