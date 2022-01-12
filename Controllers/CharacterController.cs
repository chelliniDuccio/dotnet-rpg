using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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
        public ActionResult<List<Character>> Get()
        {
            return Ok(_characterServices.GetAllCharacter());
        }
        
        [HttpGet("{get}")]
        public ActionResult<Character> GetSingle(int id)
        {
            return Ok(_characterServices.GetCharacterById(id));
        }

        [HttpPost]
        public ActionResult<List<Character>> AddCharacter(Character newCharacter)
        {
            return Ok(_characterServices.AddCharacter(newCharacter));
        }
    }
}