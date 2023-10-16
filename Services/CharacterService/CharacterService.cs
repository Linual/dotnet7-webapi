using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet7.Models;
using webapi7.Controllers;

namespace webapi7.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private static List<Character> characters = new List<Character>{
            new Character(),
            new Character { Id = 1, Name = "Sam"}
        };

        public async Task<ServiceResponce<List<Character>>> AddCharacter(Character newCharacter)
        {
            var serviceResponce = new ServiceResponce<List<Character>>();
            characters.Add(newCharacter);
            serviceResponce.Data = characters;
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<Character>>> GetAllCharacters()
        {
            var serviceResponce = new ServiceResponce<List<Character>>();
            serviceResponce.Data = characters;
            return serviceResponce;
        }

        public async Task<ServiceResponce<Character>> GetCharacterById(int id)
        {
            var serviceResponce = new ServiceResponce<Character>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponce.Data = character;
            return serviceResponce;
        }
    }
}