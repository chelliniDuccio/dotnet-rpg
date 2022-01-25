using AutoMapper;
using dotnet_rpg.Models;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Dtos.Skill;

namespace dotnet_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, AddWeaponDto>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}
