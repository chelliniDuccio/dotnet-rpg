using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private  static List<Character> characters = new List<Character>{
            new Character(),
            new Character{Name = "Duccio"}
        };


        [HttpGet]
        [Route("GetAll")]
        public ActionResult<List<Character>> Get()
        {
            return Ok(characters);
        }
        [HttpGet]
        public ActionResult<Character> GetSingle(int id)
        {
            return Ok(characters[id]);
        }
    }
}