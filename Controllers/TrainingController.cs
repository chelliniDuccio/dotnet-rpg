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

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<TrainingService>>> Training(int characterId, TrainingType trainingType)
        {
            return Ok(await _trainingService.Training(characterId, trainingType));
        }
    }
}
