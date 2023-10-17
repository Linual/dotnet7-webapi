using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet7.Models;
using Microsoft.AspNetCore.Mvc;

namespace webapi7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<ServiceResponce<List<GetCharacterDto>>>> Get()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponce<GetCharacterDto>>> GetSingle(int id)
        {
            return Ok(await _characterService.GetCharacterById(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponce<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponce<List<GetCharacterDto>>>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var responce = await _characterService.UpdateCharacter(updatedCharacter);
            if (responce.Data is null)
            {
                return NotFound(responce);
            }
            return Ok(responce);
        }

        [HttpDelete("id")]
        public async Task<ActionResult<ServiceResponce<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var responce = await _characterService.DeleteCharacter(id);
            if (responce.Data is null)
            {
                return NotFound(responce);
            }
            return Ok(responce);
        }
    }
}