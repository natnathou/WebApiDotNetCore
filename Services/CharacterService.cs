using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Services;
using dotnet_rpg.Models;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using AutoMapper;

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
            listCharacters.Add(_mapper.Map<Character>(newCharacter));
            response.Data = (listCharacters.Select(l => _mapper.Map<GetCharacterDto>(l))).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            response.Data = (listCharacters.Select(l => _mapper.Map<GetCharacterDto>(l))).ToList();
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetOneCharacter(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            response.Data = _mapper.Map<GetCharacterDto>(listCharacters.FirstOrDefault(l => l.Id == id));
            return response;
        }
    }
}