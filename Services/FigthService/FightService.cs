using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.FigthService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;

        public FightService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();

            try
            {
                var attacker =  _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefault(c => c.Id == request.AttackerId);

                var opponent = _context.Characters
                    .FirstOrDefault(c => c.Id == request.OpponentId);

                if(attacker == null || opponent == null)
                {
                    response.Success = false;
                    response.Message = "Attacker or Opponer not found";
                    return response;
                }



                var attackDamage = attacker.Weapon.Damage + (new Random().Next(attacker.Streigth)) - (new Random().Next(opponent.Defence));
                attackDamage = (attackDamage <= 0) ? 0 : attackDamage;

                if (attackDamage > 0) opponent.HitPoint -= attackDamage;
                if (opponent.HitPoint < 0) response.Message = $"{opponent.Name} has been defeated";

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHp = attacker.HitPoint,
                    OpponentHp = opponent.HitPoint - attackDamage,
                    Damage =  attackDamage
                };
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
