using AutoMapper;
using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Entities;

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
            CreateMap<User, UserDto>()
                .ReverseMap();
        }
    }
}
