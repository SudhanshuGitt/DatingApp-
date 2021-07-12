using System.Linq;
using API.DTOs;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    // Helps us to map from one object to another
    public AutoMapperProfiles()
    {
      // Appuser mapped to MemberDto 
      // to add photo url in the Member dto  
      CreateMap<AppUser, MemberDto>()
      // what property we are lokking to effect
      // in options we can tell where we need it to map from
         .ForMember(dest => dest.PhotoUrl, opt => opt.MapFrom(src =>
            src.Photos.FirstOrDefault(x => x.IsMainPhoto==true).Url))
         .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.            DateOfBirth.CalculateAge()));
      CreateMap<Photo, PhotoDto>();
    }
  }
}