using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet7.Models;
using webapi7.Dtos.Weapon;

namespace webapi7.Services.WeaponService
{
    public interface IWeaponService
    {
        Task<ServiceResponce<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
    }
}