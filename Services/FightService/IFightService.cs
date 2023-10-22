using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet7.Dtos.Fight;
using dotnet7.Models;

namespace dotnet7.Services.FightService
{
    public interface IFightService
    {
        Task<ServiceResponce<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
        Task<ServiceResponce<AttackResultDto>> SkillAttack(SkillAttackDto request);
        Task<ServiceResponce<FightResultDto>> Fight(FightRequestDto request);
        Task<ServiceResponce<List<HighscoreDto>>> GetHighscore();
    }
}