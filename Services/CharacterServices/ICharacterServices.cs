using dotnet_rpg.Models;
using dotnet_rpg.Dtos.Character;
using System.Threading.Tasks;
using dotnet_rpg.Dtos;

namespace dotnet_rpg.Services.CharacterServices;

public interface ICharacterServices
{
    Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
    Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
    Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);
    Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacter);
    Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
    Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill);
}
