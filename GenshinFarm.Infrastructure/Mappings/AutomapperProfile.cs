using AutoMapper;
using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinFarm.Infrastructure.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Character, CharacterDto>()
                .ReverseMap();
            CreateMap<Weapon, WeaponDto>()
                .ReverseMap();
            CreateMap<Material, MaterialDto>()
                .ReverseMap();
            CreateMap<FarmLocation, FarmLocationDto>()
                .ReverseMap();
            CreateMap<Talent, TalentDto>()
                .ReverseMap();
        }
    }
}
