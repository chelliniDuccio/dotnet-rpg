using dotnet_rpg.Models;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Services.CharacterServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Dtos;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterServices _characterServices;

        public CharacterController(ICharacterServices characterServices)
        {
            _characterServices = characterServices;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<AddCharacterDto>>>> Get()
        {
            return Ok(await _characterServices.GetAllCharacter());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddCharacterDto>> GetSingle(int id)
        {
            return Ok(await _characterServices.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<List<AddCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterServices.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpDateCharacter(UpdateCharacterDto updateCharacter)
        {
            var response = await _characterServices.UpdateCharacter(updateCharacter);
            if (response.Data == null)
            {
                return BadRequest(response);
            }
            else
            {
                return Ok(response);
            }
        }
    }
}