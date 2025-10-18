using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using NetTopologySuite.IO;
using Newtonsoft.Json;

namespace BlizuTebe.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<Announcement, AnnouncementDto>();
            CreateMap<AnnouncementDto, Announcement>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Picture, opt => opt.Ignore());
            CreateMap<Announcement, AnnouncementDto>()
                .ForMember(dest => dest.Picture, opt => opt.Ignore())
                .ForMember(dest => dest.ExistingPicture, opt => opt.MapFrom(src => src.Picture));
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore()) // Uvek ignoriši promenu lozinke kroz opšti DTO
            .ForMember(dest => dest.IsVerified, opt => opt.Ignore()) // Uvek ignoriši promenu sistemskih polja
            .ForMember(dest => dest.Role, opt => opt.Ignore());

           
        }
    }
}
