using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterServices;

public interface ICharacterServices
{
    List<Character> GetAllCharacter();
    Character GetCharacterById(int id);
    List<Character> AddCharacter(Character newCharacter);
}
