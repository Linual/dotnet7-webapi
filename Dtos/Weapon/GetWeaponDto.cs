using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi7.Dtos.Weapon
{
    public class GetWeaponDto
    {
        public string Name { get; set; } = string.Empty;
        public int Damage { get; set; }
    }
}