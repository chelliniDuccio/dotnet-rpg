using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace dotnet_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeaponService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int getUserID() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));


        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {

                var character =  _context.Characters
                    .FirstOrDefault(c => c.Id == newWeapon.CharacterId
                        && c.User.Id == getUserID()
                        && c.Weapon == null);

                if (character == null)
                {
                    response.Success = false;
                    response.Message = "Character not found";
                    return response;
                }

                var weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character
                };

                _context.Weapons.Add(weapon);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);

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
