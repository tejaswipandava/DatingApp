using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<appUser, MemberDto>()
            .ForMember(
                desc => desc.PhotoUrl, opt => opt.MapFrom(
                     src => src.photo.FirstOrDefault(x => x.IsMain).Url))
            .ForMember(desc => desc.Age, opt => opt.MapFrom(
                src => src.DOB.CalculateAge()
            ));
            CreateMap<Photo, PhotoDto>();
            CreateMap<MemberUpdateDto, appUser>();
        }
    }
}