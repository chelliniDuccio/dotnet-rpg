using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Models;
using System.Collections.Generic;

namespace dotnet_rpg.Services.FigthService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
        Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
        Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request);
        Task<ServiceResponse<FightResultDto>> BattleRoyale(FightRequestDto request);
        Task<ServiceResponse<FightResultDto>> TeamBatle(List<List<int>> request);
        Task<ServiceResponse<FightResultDto>> BossFight(FightRequestDto request);
        Task<ServiceResponse<List<HightscoreDto>>> HightScore();
    }
}
