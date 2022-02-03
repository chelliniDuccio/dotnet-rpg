using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Training;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace dotnet_rpg.Services.TrainingService
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TrainingType
    {
        Strength,
        Defence,
        Intelligence,
        WeaponDamage
    }

    public class TrainingService : ITrainingService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TrainingService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int getUserID() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        private int characterStats(Character character) => character.Streigth + character.Defence + character.Intelligence + character.Weapon.Damage;

        public async Task<ServiceResponse<TrainingDto>> Training(int characterId, TrainingType trainingType)
        {
            var response = new ServiceResponse<TrainingDto>();

            try
            {
                int maxTotal = 230;

                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == characterId && c.User.Id == getUserID());

                CharacterClassConfiguration configuration = await _context.ClassConfigurations
                    .FirstOrDefaultAsync(c => c.Class == character.Class);

                if (character == null || configuration == null)
                {
                    response.Success = false;
                    response.Message = $"Character or configuration not found";
                    return response;
                }

                var characterValues = characterStats(character);
                if (characterValues >= maxTotal)
                {
                    response.Success = false;
                    response.Message = "Character is yet enough strong";
                    return response;
                }

                var maxTraining = characterStats(character) <= 225 ? 5 : 230 - characterStats(character);
                var training = new Random().Next(1, maxTraining);

                switch (trainingType)
                {
                    case TrainingType.Strength:
                        character.Streigth += training;
                        if (character.Streigth > configuration.MaxStrength) character.Streigth = configuration.MaxStrength;
                        response.Message = new string($"{character.Name}'s strngth has been incremented by {training} ({character.Streigth}⚔)");
                        break;
                    case TrainingType.Defence:
                        character.Defence += training;
                        if (character.Defence > configuration.MaxDefence) character.Defence = configuration.MaxDefence;
                        response.Message = new string($"{character.Name}'s defence has been incremented by {training} ({character.Defence}🛡)");
                        break;
                    case TrainingType.Intelligence:
                        character.Intelligence += training;
                        if (character.Intelligence > configuration.MaxIntelligence) character.Intelligence = configuration.MaxIntelligence;
                        response.Message = new string($"{character.Name}'s intelligence has been incremented by {training} ({character.Intelligence}📜)");
                        break;
                    case TrainingType.WeaponDamage:
                        character.Weapon.Damage += training;
                        if (character.Weapon.Damage > configuration.MaxWeaponDamage) character.Weapon.Damage = configuration.MaxWeaponDamage;
                        response.Message = new string($"{character.Name}'s weapon ({character.Weapon.Name}) damage has been incremented by {training} ({character.Weapon.Damage}🏹)");
                        break;
                    default:
                        break;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
