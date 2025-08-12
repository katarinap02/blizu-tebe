using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;

namespace BlizuTebe.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Announcement, AnnouncementDto>();
            CreateMap<AnnouncementDto, Announcement>();
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
            .ForMember(dest => dest.Password, opt => opt.Ignore()) // Uvek ignoriši promenu lozinke kroz opšti DTO
            .ForMember(dest => dest.IsVerified, opt => opt.Ignore()) // Uvek ignoriši promenu sistemskih polja
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        }
    }
}
