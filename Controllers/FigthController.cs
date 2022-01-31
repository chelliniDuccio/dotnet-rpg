using dotnet_rpg.Dtos.Fight;
using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Models;
using dotnet_rpg.Services.FigthService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class FigthController : ControllerBase
    {
        private readonly IFightService _fightService;

        public FigthController(IFightService fightService)
        {
            _fightService = fightService;
        }

        [HttpPost("Weapon")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponAttack(WeaponAttackDto request)
        {
            return Ok(await _fightService.WeaponAttack(request));
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> WeaponSkill(SkillAttackDto request)
        {
            return Ok(await _fightService.SkillAttack(request));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> Fight(FightRequestDto request)
        {
            return Ok(await _fightService.Fight(request));
        }

        [HttpPost("BattleRoyale")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> BattleRoyale(FightRequestDto request)
        {
            return Ok(await _fightService.BattleRoyale(request));
        }

        [HttpPost("TeamBattle")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> TeamBattle(List<List<int>> request)
        {
            return Ok(await _fightService.TeamBatle(request));
        }

        [HttpPost("BossFight")]
        public async Task<ActionResult<ServiceResponse<AttackResultDto>>> BossFight(FightRequestDto request)
        {
            return Ok(await _fightService.BossFight(request));
        }

        [HttpGet("Statistics")]
        public async Task<ActionResult<ServiceResponse<HightscoreDto>>> GetHighscore()
        {
            return Ok(await _fightService.HightScore());
        }
    }
}
