global using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet7.Models;
using webapi7.Controllers;

namespace webapi7.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<ServiceResponce<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharacter)
        {
            var serviceResponce = new ServiceResponce<List<GetCharacterDto>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponce.Data = 
                await _context.Characters
                .Where(c => c.User!.Id == GetUserId())
                .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            var serviceResponce = new ServiceResponce<List<GetCharacterDto>>();

            try
            {
                var character = 
                    await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
                if (character is null)
                    throw new Exception($"Character with Id '{id}' not found");

                _context.Characters.Remove(character);
                
                await _context.SaveChangesAsync();
                
                serviceResponce.Data = 
                    await _context.Characters
                    .Where(c => c.User!.Id == GetUserId())
                    .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = ex.Message;
            }

            return serviceResponce;
        }

        public async Task<ServiceResponce<List<GetCharacterDto>>> GetAllCharacters()
        {
            var serviceResponce = new ServiceResponce<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.User!.Id == GetUserId()).ToListAsync();
            serviceResponce.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponce;
        }

        public async Task<ServiceResponce<GetCharacterDto>> GetCharacterById(int id)
        {
            var serviceResponce = new ServiceResponce<GetCharacterDto>();
            var dbCharacter = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserId());
            serviceResponce.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
            return serviceResponce;
        }

        public async Task<ServiceResponce<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            var serviceResponce = new ServiceResponce<GetCharacterDto>();

            try {
                var character = 
                    await _context.Characters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (character is null || character.User!.Id != GetUserId())
                    throw new Exception($"Character with Id '{updatedCharacter.Id}' not found.");

                _mapper.Map(updatedCharacter, character);
                
                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defence = updatedCharacter.Defence;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                await _context.SaveChangesAsync();

                serviceResponce.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                serviceResponce.Success = false;
                serviceResponce.Message = ex.Message;
            }
            return serviceResponce;
        }

        public async Task<ServiceResponce<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
            var responce = new ServiceResponce<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterId &&
                        c.User!.Id == GetUserId());

                if (character is null)
                {
                    responce.Success = false;
                    responce.Message = "Character not found.";
                    return responce;
                }

                var skill = await _context.Skills
                    .FirstOrDefaultAsync(s => s.Id == newCharacterSkill.SkillId);
                if (skill is null)
                {
                    responce.Success = false;
                    responce.Message = "Skill not found.";
                    return responce;
                }

                character.Skills!.Add(skill);
                await _context.SaveChangesAsync();
                responce.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                responce.Success = false;
                responce.Message = ex.Message;
            }

            return responce;
        }
    }
}