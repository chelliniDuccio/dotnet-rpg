using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Training;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace dotnet_rpg.Services.TrainingService
{
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

        private async Task<Character> GetCharacterAsync(int id)
        {
            var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == getUserID());
            return character;
        }

        private int characterStats(Character character) => character.Streigth + character.Defence + character.Intelligence + character.Weapon.Damage;

        public async Task<ServiceResponse<TrainingDto>> DefenceTraining(int characterId)
        {
            var response = new ServiceResponse<TrainingDto>();

            try
            {
                var character = await GetCharacterAsync(characterId);

                if(character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                var characterValues = characterStats(character);
                var training = new Random().Next(1, characterValues < 225 ? 5 : 230 - characterValues);
                if (training <= 0)
                {
                    response.Success = false;
                    response.Message = "Character is yet enough strong";
                    return response;
                }

                character.Defence += training;
                response.Message = new string($"{character.Name}'s defence has been incremented by {training} ({character.Defence}🛡)");  

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<TrainingDto>> IntelligenceTraining(int characterId)
        {
            var response = new ServiceResponse<TrainingDto>();

            try
            {
                var character = await GetCharacterAsync(characterId);

                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                var characterValues = characterStats(character);
                var training = new Random().Next(characterValues < 225 ? 5 : 230 - characterValues);
                if (training <= 0)
                {
                    response.Success = false;
                    response.Message = "Character is yet enough strong";
                    return response;
                }

                character.Intelligence += training;
                response.Message = new string($"{character.Name}'s intelligence has been incremented by {training} ({character.Intelligence}📜)");

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<TrainingDto>> StrengthTraining(int characterId)
        {
            var response = new ServiceResponse<TrainingDto>();

            try
            {
                var character = await GetCharacterAsync(characterId);

                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                var characterValues = characterStats(character);
                var training = new Random().Next(characterValues < 225 ? 5 : 230 - characterValues);
                if (training <= 0)
                {
                    response.Success = false;
                    response.Message = "Character is yet enough strong";
                    return response;
                }

                character.Streigth += training;
                response.Message = new string($"{character.Name}'s strngth has been incremented by {training} ({character.Streigth}⚔)");

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<TrainingDto>> WeaponTraining(int characterId)
        {
            var response = new ServiceResponse<TrainingDto>();

            try
            {
                var character = await GetCharacterAsync(characterId);

                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                var characterValues = characterStats(character);
                var training = new Random().Next(characterValues < 225 ? 5 : 230 - characterValues);
                if (training <= 0)
                {
                    response.Success = false;
                    response.Message = "Character is yet enough strong";
                    return response;
                }

                character.Weapon.Damage += training;
                response.Message = new string($"{character.Name}'s weapon ({character.Weapon.Name}) damage has been incremented by {training} ({character.Weapon.Damage}🏹)");

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
