using dotnet_rpg.Dtos.Training;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.TrainingService
{
    public interface ITrainingService
    {
        Task<ServiceResponse<TrainingDto>> StrengthTraining(int characterId);
        Task<ServiceResponse<TrainingDto>> DefenceTraining(int characterId);
        Task<ServiceResponse<TrainingDto>> IntelligenceTraining(int characterId);
        Task<ServiceResponse<TrainingDto>> WeaponTraining(int characterId);
    }
}
