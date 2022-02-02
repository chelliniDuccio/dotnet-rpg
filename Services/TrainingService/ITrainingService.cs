using dotnet_rpg.Dtos.Training;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.TrainingService
{
    public interface ITrainingService
    {
        Task<ServiceResponse<TrainingDto>> Training(int characterId, TrainingType trainingType);

    }
}
