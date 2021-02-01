using Microsoft.AspNetCore.Mvc;
using dotnet_rpg.Models;
using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Services;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;

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
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
      this._characterService = characterService;
    }

    // [Route("getall")]
    public async Task<IActionResult> GetAll()
    {

      return Ok(await _characterService.GetAllCharacters());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {

      return Ok(await _characterService.GetCharacter(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post(AddCharacterDto newCharacter)
    {

      return Ok(await _characterService.AddCharacter(newCharacter));
    }

    [HttpPut]
    public async Task<IActionResult> Put(GetCharacterDto characterToModify)
    {

      return Ok(await _characterService.UpdateCharacter(characterToModify));
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Del(int id)
    {
      return Ok(await _characterService.DeleteCharacter(id));
    }
  }
}