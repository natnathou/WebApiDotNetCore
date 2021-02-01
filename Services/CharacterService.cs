using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Services;
using dotnet_rpg.Models;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using AutoMapper;
using System;

namespace dotnet_rpg.Services
{
  public class CharacterService : ICharacterService
  {
    private static List<Character> listCharacters = new List<Character>{
            new Character(),
            new Character(){Id=2, Name="Patrick"}
        };
    private readonly IMapper _mapper;

    public CharacterService(IMapper mapper)
    {
      _mapper = mapper;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
    {
      var response = new ServiceResponse<List<GetCharacterDto>>();

      var maxId = listCharacters.Max(l => l.Id);
      var character = _mapper.Map<Character>(newCharacter);
      character.Id = maxId + 1;
      listCharacters.Add(character);

      response.Data = (listCharacters.Select(l => _mapper.Map<GetCharacterDto>(l))).ToList();
      return response;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
    {
      var response = new ServiceResponse<List<GetCharacterDto>>();
      response.Data = (listCharacters.Select(l => _mapper.Map<GetCharacterDto>(l))).ToList();
      return response;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> UpdateCharacter(GetCharacterDto characterToModify)
    {
      var response = new ServiceResponse<List<GetCharacterDto>>();

      try
      {
        var characterSelected = listCharacters.Find(l => l.Id == characterToModify.Id);
        characterSelected.HitPoints = characterToModify.HitPoints;
        characterSelected.Class = characterToModify.Class;
        characterSelected.Defense = characterToModify.Defense;
        characterSelected.Intelligence = characterToModify.Intelligence;
        characterSelected.Name = characterToModify.Name;
        characterSelected.Strength = characterToModify.Strength;

      }
      catch (Exception exception)
      {
        response.Success = false;
        response.Message = exception.Message;
      }


      response.Data = (listCharacters.Select(l => _mapper.Map<GetCharacterDto>(l))).ToList();
      return response;
    }


    public async Task<ServiceResponse<GetCharacterDto>> GetCharacter(int id)
    {
      var response = new ServiceResponse<GetCharacterDto>();
      response.Data = _mapper.Map<GetCharacterDto>(listCharacters.FirstOrDefault(l => l.Id == id));
      return response;
    }

    public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
    {
      var response = new ServiceResponse<List<GetCharacterDto>>();
      listCharacters.Remove(listCharacters.Find(l => l.Id == id));
      response.Data = (listCharacters.Select(l => _mapper.Map<GetCharacterDto>(l))).ToList();
      return response;
    }


  }
}