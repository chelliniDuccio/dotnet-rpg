using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Dtos.Skill;
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
                var attacker = await _context.Characters
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                if (attacker == null || opponent == null)
                {
                    response.Success = false;
                    response.Message = "Attacker or Opponer not found";
                    return response;
                }

                int attackDamage = WeaponAttack(attacker, opponent);
                if (opponent.HitPoint < 0) response.Message = $"{opponent.Name} has been defeated";

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHp = attacker.HitPoint,
                    OpponentHp = opponent.HitPoint - attackDamage,
                    Damage = attackDamage
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        private static int WeaponAttack(Character? attacker, Character? opponent)
        {
            var attackDamage = attacker.Weapon.Damage + (new Random().Next(attacker.Streigth)) - (new Random().Next(opponent.Defence));
            attackDamage = (attackDamage <= 0) ? 0 : attackDamage;
            if (attackDamage > 0) opponent.HitPoint -= attackDamage;
            return attackDamage;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
            var response = new ServiceResponse<AttackResultDto>();

            try
            {
                var attacker = await _context.Characters
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

                var opponent = await _context.Characters
                                    .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

                if (attacker == null || opponent == null)
                {
                    response.Success = false;
                    response.Message = "Attacker or Opponer not found";
                    return response;
                }

                var skill = attacker.Skills.FirstOrDefault(s => s.Id == request.SkillId);

                if (skill == null)
                {
                    response.Success = false;
                    response.Message = $"{attacker.Name} doesn't know the skill.";
                    return response;
                }

                int attackDamage = SkillAttack(attacker, opponent, skill);
                if (opponent.HitPoint < 0) response.Message = $"{opponent.Name} has been defeated";

                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                    Attacker = attacker.Name,
                    Opponent = opponent.Name,
                    AttackerHp = attacker.HitPoint,
                    OpponentHp = opponent.HitPoint - attackDamage,
                    Damage = attackDamage
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        private static int SkillAttack(Character? attacker, Character? opponent, Skill? skill)
        {
            var attackDamage = skill.Damage + (new Random().Next(attacker.Streigth)) - (new Random().Next(opponent.Defence));
            attackDamage = (attackDamage <= 0) ? 0 : attackDamage;
            if (attackDamage > 0) opponent.HitPoint -= attackDamage;
            return attackDamage;
        }

        public async Task<ServiceResponse<FightResultDto>> Fight(FightRequestDto request)
        {
            var response = new ServiceResponse<FightResultDto> { Data = new FightResultDto() };

            try
            {
                var characters = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .Where(c => request.CharatctersIds.Contains(c.Id)).ToListAsync();

                if(characters.Count == 0)
                {
                    response.Success=false;
                    response.Message = "You need at least 2 characetrs to start a fight";
                    return response;
                }
                if (characters.Count == 1)
                {
                    response.Success = false;
                    response.Message = $"{characters[0].Name} has no apponent to fight";
                    return response;
                }

                var defeat = false;

                while (!defeat)
                {
                    foreach (var attacker in characters)
                    {
                        var opponents = characters.Where(c => c.Id != attacker.Id).ToList();
                        var opponent = opponents[new Random().Next(opponents.Count())];

                        int damage = 0;
                        string attackUsed = string.Empty;

                        bool useWeapon = new Random().Next(2) == 0;
                        if (useWeapon)
                        {
                            attackUsed = attacker.Weapon.Name;
                            damage = WeaponAttack(attacker, opponent);
                        }
                        else
                        {
                            if(attacker.Skills.Count == 0)
                            {
                                response.Data.Log.Add($"{attacker.Name} isn't prepared to fight");
                                continue;
                            }
                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count() - 1)];
                            attackUsed = skill.Name;
                            damage = SkillAttack(attacker, opponent, skill);
                        }

                        response.Data.Log.Add($"{attacker.Name} attack {opponent.Name} using {attackUsed} with {damage}");

                        if (opponent.HitPoint <= 0)
                        {
                            defeat = true;
                            opponent.Defeats++;
                            attacker.Victory++;
                            response.Data.Log.Add($"⚔️⚔️ {attacker.Name} ({attacker.HitPoint}❤️) has defeated {opponent.Name}⚔️⚔️");
                            break;
                        }
                    }
                }
                characters.ForEach(c => { c.Fights++; c.HitPoint = 100; });
                _context.SaveChangesAsync();
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
