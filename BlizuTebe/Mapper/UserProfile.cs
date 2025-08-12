using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;

namespace BlizuTebe.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()/*
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.IsVerified, opt => opt.Ignore())*/;
        }
    }
}
