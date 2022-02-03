using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.CharacterClassConfiguration;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterClassConfigurationService
{
    public class CharacterClassConfigurationService : ICharacterClassConfigurationService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CharacterClassConfigurationService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<CharacterClassConfiguration>> AddConfiguration(CharacterClassConfigurationDto newConfigurations)
        {
            var response = new ServiceResponse<CharacterClassConfiguration>();

            try
            {
                var existingConfiguration = await _context.ClassConfigurations.FirstOrDefaultAsync(c => c.Class == newConfigurations.Class);

                if(existingConfiguration != null)
                {
                    response.Success = false;
                    response.Message = $"A configuration yet exist for {newConfigurations.Class} class";
                    return response;
                }

                var configuration = _mapper.Map<CharacterClassConfiguration>(newConfigurations);
                _context.ClassConfigurations.Add(configuration);
                await _context.SaveChangesAsync();
                response.Data = configuration;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<CharacterClassConfiguration>> DeleteConfiguration(RpgClass classConfig)
        {
            var response = new ServiceResponse<CharacterClassConfiguration>();

            try
            {
                var configuration = await _context.ClassConfigurations
                    .FirstOrDefaultAsync(c => c.Class == classConfig);

                _context.ClassConfigurations.Remove(configuration);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<CharacterClassConfiguration>>> GetAllConfigurations()
        {
            var response = new ServiceResponse<List<CharacterClassConfiguration>>();

            try
            {
                var configurations = await _context.ClassConfigurations.ToListAsync();

                if (configurations == null)
                {
                    response.Success = false;
                    response.Message = "No configuration found";
                    return response;
                }
                response.Data = configurations;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<CharacterClassConfiguration>> GetConfigurationByClass(RpgClass classConfig)
        {
            var response = new ServiceResponse<CharacterClassConfiguration>();

            try
            {
                var configuration = await _context.ClassConfigurations
                    .FirstOrDefaultAsync(c => c.Class == classConfig);

                if (configuration == null)
                {
                    response.Success = false;
                    response.Message = $"No configuration found for {classConfig} class";
                    return response;
                }

                response.Data = configuration;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<CharacterClassConfiguration>> UpdateConfiguration(CharacterClassConfigurationDto updateConfigurations)
        {
            var response = new ServiceResponse<CharacterClassConfiguration>();

            try
            {
                var configuration = await _context.ClassConfigurations
                    .FirstOrDefaultAsync(c => c.Class == updateConfigurations.Class);
                if (configuration == null)
                {
                    response.Success = false;
                    response.Message = $"No configuration found for {updateConfigurations} class";
                    return response;
                }

                configuration.MaxStrength = updateConfigurations.MaxStrength;
                configuration.MaxDefence = updateConfigurations.MaxDefence;
                configuration.MaxIntelligence = updateConfigurations.MaxIntelligence;
                configuration.MaxWeaponDamage = updateConfigurations.MaxWeaponDamage;

                await _context.SaveChangesAsync();
                response.Data = configuration;
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
