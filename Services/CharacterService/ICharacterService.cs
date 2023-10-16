using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet7.Models;

namespace webapi7.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponce<List<Character>>> GetAllCharacters();
        Task<ServiceResponce<Character>> GetCharacterById(int id);
        Task<ServiceResponce<List<Character>>> AddCharacter(Character newCharacter);
    }
}