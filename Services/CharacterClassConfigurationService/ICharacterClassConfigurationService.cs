using dotnet_rpg.Dtos.CharacterClassConfiguration;
using dotnet_rpg.Models;
using System.Collections.Generic;

namespace dotnet_rpg.Services.CharacterClassConfigurationService
{
    public interface ICharacterClassConfigurationService
    {
        Task<ServiceResponse<List<CharacterClassConfiguration>>> GetAllConfigurations();
        Task<ServiceResponse<CharacterClassConfiguration>> GetConfigurationByClass(RpgClass classConfig);
        Task<ServiceResponse<CharacterClassConfiguration>> AddConfiguration(CharacterClassConfigurationDto newConfigurations);
        Task<ServiceResponse<CharacterClassConfiguration>> UpdateConfiguration(CharacterClassConfigurationDto updateConfigurations);
        Task<ServiceResponse<CharacterClassConfiguration>> DeleteConfiguration(RpgClass classConfig);
    }
}
