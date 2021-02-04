using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Services;
using dotnet_rpg.Models;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.Character;
using AutoMapper;
using System;
using dotnet_rpg.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace dotnet_rpg.Services
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> GetUser()
        {

            int userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            return userId;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();

            var character = _mapper.Map<Character>(newCharacter);
            await _context.AddAsync(character);
            await _context.SaveChangesAsync();

            response.Data = (_context.Characters.Select(l => _mapper.Map<GetCharacterDto>(l))).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            int userId = await GetUser();
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.Where(c => c.User.Id == userId).ToListAsync();
            response.Data = (dbCharacters.Select(l => _mapper.Map<GetCharacterDto>(l))).ToList();
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> UpdateCharacter(GetCharacterDto characterToModify)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                Character characterSelected = await _context.Characters.FirstAsync(l => l.Id == characterToModify.Id);
                characterSelected.HitPoints = characterToModify.HitPoints;
                characterSelected.Class = characterToModify.Class;
                characterSelected.Defense = characterToModify.Defense;
                characterSelected.Intelligence = characterToModify.Intelligence;
                characterSelected.Name = characterToModify.Name;
                characterSelected.Strength = characterToModify.Strength;

                _context.Characters.Update(characterSelected);
                await _context.SaveChangesAsync();

            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }


            response.Data = (_context.Characters.Select(l => _mapper.Map<GetCharacterDto>(l))).ToList();
            return response;
        }


        public async Task<ServiceResponse<GetCharacterDto>> GetCharacter(int id)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            response.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var response = new ServiceResponse<List<GetCharacterDto>>();
            _context.Characters.Remove(await _context.Characters.FirstAsync(l => l.Id == id));
            await _context.SaveChangesAsync();
            response.Data = (_context.Characters.Select(l => _mapper.Map<GetCharacterDto>(l))).ToList();
            return response;
        }


    }
}