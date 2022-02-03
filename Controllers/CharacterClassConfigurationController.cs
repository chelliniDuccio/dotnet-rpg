using dotnet_rpg.Dtos.CharacterClassConfiguration;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterClassConfigurationService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CharacterClassConfigurationController : ControllerBase
    {
        private readonly ICharacterClassConfigurationService _configurationService;

        public CharacterClassConfigurationController(ICharacterClassConfigurationService configurationService)
        {
            _configurationService = configurationService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CharacterClassConfiguration>>>> Get()
        {
            return Ok(await _configurationService.GetAllConfigurations());
        }

        [HttpGet("Class")]
        public async Task<ActionResult<CharacterClassConfiguration>> GetSingle(RpgClass classConfig)
        {
            return Ok(await _configurationService.GetConfigurationByClass(classConfig));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<CharacterClassConfiguration>>>> Add(CharacterClassConfigurationDto newConfiguration)
        {
            return Ok(await _configurationService.AddConfiguration(newConfiguration));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<CharacterClassConfiguration>>>> Update(CharacterClassConfigurationDto updateConfiguration)
        {
            return Ok(await _configurationService.UpdateConfiguration(updateConfiguration));
        }

        [HttpDelete]
        public async Task<ActionResult<ServiceResponse<List<CharacterClassConfiguration>>>> Delete(RpgClass classConfig)
        {
            return Ok(await _configurationService.DeleteConfiguration(classConfig));
        }

    }
}

