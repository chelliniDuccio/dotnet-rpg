using dotnet_rpg.Models;
using dotnet_rpg.Services.TrainingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]

    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _trainingService;

        public TrainingController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpPost("Strength")]
        public async Task<ActionResult<ServiceResponse<TrainingService>>> StrenghtTraining(int characterId)
        {
            return Ok(await _trainingService.StrengthTraining(characterId));
        }

        [HttpPost("Defence")]
        public async Task<ActionResult<ServiceResponse<TrainingService>>> DefenceTraining(int characterId)
        {
            return Ok(await _trainingService.DefenceTraining(characterId));
        }

        [HttpPost("Intelligence")]
        public async Task<ActionResult<ServiceResponse<TrainingService>>> IntelligenceTraining(int characterId)
        {
            return Ok(await _trainingService.IntelligenceTraining(characterId));
        }

        [HttpPost("Weapon")]
        public async Task<ActionResult<ServiceResponse<TrainingService>>> WeaponTraining(int characterId)
        {
            return Ok(await _trainingService.WeaponTraining(characterId));
        }
    }
}
