using System.Collections.Generic;
using System.Linq;
using dotnet_rpg.Services;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> listCharacters = new List<Character>{
            new Character(),
            new Character(){Id=2, Name="Patrick"}
        };
        List<Character> ICharacterService.AddCharacter(Character newCharacter)
        {
            listCharacters.Add(newCharacter);
            return listCharacters;
        }

        List<Character> ICharacterService.GetAllCharacters()
        {
            return listCharacters;
        }

        Character ICharacterService.GetOneCharacter(int id)
        {
            return listCharacters.FirstOrDefault(l => l.Id == id);
        }
    }
}