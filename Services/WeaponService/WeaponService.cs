using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using dotnet7.Models;
using webapi7.Dtos.Weapon;

namespace webapi7.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _context = context;
        }
        public async Task<ServiceResponce<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var responce = new ServiceResponce<GetCharacterDto>();
            try
            {
                var character = await _context.Characters
                    .FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId &&
                        c.User!.Id == int.Parse(_httpContextAccessor.HttpContext!.User
                            .FindFirstValue(ClaimTypes.NameIdentifier)!));
                            
                if (character is null)
                {
                    responce.Success = false;
                    responce.Message = "Character not found.";
                    return responce;
                }

                var weapon = new Weapon
                {
                    Name = newWeapon.Name,
                    Damage = newWeapon.Damage,
                    Character = character
                };

                _context.Weapons.Add(weapon);
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