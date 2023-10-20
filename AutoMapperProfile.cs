using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi7.Dtos.Skill;
using webapi7.Dtos.Weapon;

namespace webapi7
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<UpdateCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
        }
    }
}