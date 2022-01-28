using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_rpg.Services.FigthService
{
    public class FightService : IFightService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FightService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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
            var attackDamage = skill.Damage + (new Random().Next(attacker.Streigth)) - (new Random().Next(opponent.Intelligence));
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

                if (characters.Count == 0)
                {
                    response.Success = false;
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
                            if (attacker.Skills.Count == 0)
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
                            attacker.Victories++;
                            response.Data.Log.Add($"⚔️⚔️ {attacker.Name} ({attacker.HitPoint}❤️) has defeated {opponent.Name} ⚔️⚔️");
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

        public async Task<ServiceResponse<FightResultDto>> BattleRoyale(FightRequestDto request)
        {
            var response = new ServiceResponse<FightResultDto> { Data = new FightResultDto() };

            try
            {
                var characters = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .Where(c => request.CharatctersIds.Contains(c.Id)).ToListAsync();

                if (characters.Count == 0)
                {
                    response.Success = false;
                    response.Message = "You need at least 2 characetrs to start a fight";
                    return response;
                }
                if (characters.Count == 1)
                {
                    response.Success = false;
                    response.Message = $"{characters[0].Name} has no apponent to fight";
                    return response;
                }

                var fightigCharacters = new List<Character>();
                characters.ForEach(c => fightigCharacters.Add(c));

                while (fightigCharacters.Count != 1)
                {
                    foreach (var attacker in fightigCharacters.ToList())
                    {
                        var opponents = fightigCharacters.Where(c => c.Id != attacker.Id).ToList();
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
                            if (attacker.Skills.Count == 0)
                            {
                                response.Data.Log.Add($"{attacker.Name} isn't prepared to fight");
                                continue;
                            }
                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count())];
                            attackUsed = skill.Name;
                            damage = SkillAttack(attacker, opponent, skill);
                        }

                        response.Data.Log.Add($"{attacker.Name} attack {opponent.Name} using {attackUsed} with {damage}");

                        if (opponent.HitPoint <= 0)
                        {
                            response.Data.Log.Add($"☠️☠️ {attacker.Name} has defeated {opponent.Name} ☠️☠️");
                            opponent.Defeats++;
                            fightigCharacters.Remove(opponent);
                            if (fightigCharacters.Count == 1)
                            {
                                attacker.Victories++;
                                response.Data.Log.Add($"🏆⚔️ {attacker.Name} ({attacker.HitPoint}❤️) is the strongher ⚔️🏆");
                                break;
                            }
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

        public async Task<ServiceResponse<FightResultDto>> TeamBatle(List<List<int>> request)
        {
            var response = new ServiceResponse<FightResultDto> { Data = new FightResultDto() };

            try
            {
                var teams = new List<List<Character>>();

                foreach (var teamIds in request)
                {
                    var team = new List<Character>();
                    foreach (var characterId in teamIds)
                    {
                        team.Add(await _context.Characters
                            .Include(c => c.Weapon)
                            .Include(c => c.Skills)
                            .FirstOrDefaultAsync(c => c.Id == characterId));
                    }
                    teams.Add(team);
                }

                if (teams.Count < 2)
                {
                    response.Success = false;
                    response.Message = "You need at least 2 teams to start a fight";
                    return response;
                }

                var fightigTeams = new List<List<Character>>();
                teams.ForEach(t => fightigTeams.Add(t));

                while (fightigTeams.ToList().Count != 1)
                {
                    foreach (var attackerTeam in fightigTeams.ToList().Where(t => t.Count > 0))
                    {
                        var attacker = attackerTeam[new Random().Next(attackerTeam.Count)];
                        var opponents = fightigTeams.Where(t => t != attackerTeam).ToList();
                        var opponentTeam = opponents[new Random().Next(fightigTeams.Count() - 1)];
                        var opponent = opponentTeam[new Random().Next(opponents.Count() - 1)];

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
                            if (attacker.Skills.Count == 0)
                            {
                                response.Data.Log.Add($"{attacker.Name} isn't prepared to fight");
                                continue;
                            }
                            var skill = attacker.Skills[new Random().Next(attacker.Skills.Count())];
                            attackUsed = skill.Name;
                            damage = SkillAttack(attacker, opponent, skill);
                        }

                        response.Data.Log.Add($"{attacker.Name} attack {opponent.Name} using {attackUsed} with {damage}");

                        if (opponent.HitPoint <= 0)
                        {
                            response.Data.Log.Add($"☠️ {attacker.Name} has defeated {opponent.Name} ☠️");
                            opponent.Defeats++;
                            opponentTeam.Remove(opponent);
                            if (opponentTeam.Count == 0)
                            {
                                response.Data.Log.Add($"🚩 {opponent.Name}'s team has lose");
                                fightigTeams.Remove(opponentTeam);
                            }
                            if (fightigTeams.Count == 1)
                            {
                                attacker.Victories++;
                                response.Data.Log.Add($"🏆⚔️ {attacker.Name}'s team has won ⚔️🏆");
                                break;
                            }
                        }
                    }
                }

                foreach (var team in request)
                {
                    foreach (var characterId in team)
                    {
                        var character = await _context.Characters
                            .FirstOrDefaultAsync(c => c.Id == characterId);
                        character.HitPoint = 100;
                    }
                }
                _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<List<HightscoreDto>>> HightScore()
        {
            var charcaracter = await _context.Characters
                .Where(c => c.Fights > 0)
                .OrderByDescending(c => c.Victories)
                .ThenBy(c => c.Defeats)
                .ToListAsync();

            var result = new ServiceResponse<List<HightscoreDto>>
            {
                Data = charcaracter.Select(c => _mapper.Map<HightscoreDto>(c)).ToList()
            };

            return result;
        }
    }
}
