using System.Collections.Generic;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services
{
  public interface ICharacterService
  {
    Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters(int id);

    Task<ServiceResponse<GetCharacterDto>> GetCharacter(int id);

    Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter);

    Task<ServiceResponse<List<GetCharacterDto>>> UpdateCharacter(GetCharacterDto newCharacter);

    Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);


  }
}