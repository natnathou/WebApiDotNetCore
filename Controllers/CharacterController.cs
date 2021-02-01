using Microsoft.AspNetCore.Mvc;
using dotnet_rpg.Models;
using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Services;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private static Character knight = new Character();

        private static List<Character> listKnight = new List<Character>{
            knight,
            new Character(){Id = 1, Name="Same"}
        };
        private readonly ICharacterService characterService;

        public CharacterController(ICharacterService characterService)
        {
            this.characterService = characterService;
        }

        [Route("getall")]
        public IActionResult GetAll()
        {

            return Ok(characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public IActionResult GetSingle(int id)
        {

            return Ok(characterService.GetOneCharacter(id));
        }

        [HttpPost]
        public IActionResult PostCharacter(Character newCharacter)
        {

            return Ok(characterService.AddCharacter(newCharacter));
        }
    }
}