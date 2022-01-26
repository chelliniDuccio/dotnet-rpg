using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.FigthService
{
    public interface IFightService
    {
        Task<ServiceResponse<AttackResultDto>>WeaponAttack(WeaponAttackDto request);
    }
}
