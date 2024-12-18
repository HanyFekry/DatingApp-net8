using AutoMapper;
using DatingApi.Dtos;
using DatingApi.Entities;
using DatingApi.Extensions;
using DatingApi.Resolvers;

namespace DatingApi.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AppUser, MemberDto>()
                .ForMember(x => x.Age, s => s.MapFrom(x => x.DateOfBirth.CalculateAge()))
                .ForMember(x => x.PhotoUrl, s => s.MapFrom(x => x.Photos.FirstOrDefault(p => p.IsMain)!.Url))
                .ForMember(dest => dest.Name, opt => opt.MapFrom<LanguageResolver>())
                .ReverseMap();
            CreateMap<Photo, PhotoDto>().ReverseMap();
            CreateMap<AppUser, MemberUpdateDto>().ReverseMap();
        }
    }
}
