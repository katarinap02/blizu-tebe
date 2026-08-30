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
            CreateMap<User, UserDto>()
            .ForMember(dest => dest.Picture, opt => opt.Ignore())
            .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture));

            CreateMap<UserDto, User>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest => dest.IsVerified, opt => opt.Ignore())
                .ForMember(dest => dest.Role, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.Rating, opt => opt.Ignore());

            CreateMap<UserViewDto, User>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.IsVerified, opt => opt.Ignore())
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.ProfilePicture, opt => opt.Ignore())
            .ForMember(dest => dest.Rating, opt => opt.Ignore());

            CreateMap<User, UserViewDto>()
                .ForMember(dest => dest.Picture, opt => opt.Ignore())
                .ForMember(dest => dest.ProfilePicture, opt => opt.MapFrom(src => src.ProfilePicture));

            CreateMap<CommunityRequest, CommunityRequestDto>()
                .ForMember(dest => dest.FilePicture, opt => opt.Ignore())
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.Picture));
            CreateMap<CommunityRequestDto, CommunityRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Picture, opt => opt.MapFrom(src => src.Picture));

            CreateMap<Discussion, DiscussionDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<CommunityRequestUsers, CommunityRequestUsersDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<DiscussionComment, DiscussionCommentDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());


            CreateMap<Rating, RatingDto>()
                .ReverseMap()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<HelpRequest, HelpRequestDto>();

            CreateMap<HelpRequestDto, HelpRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PostDate, opt => opt.Ignore())
                .ForMember(dest => dest.ExpireDate, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Attachment, opt => opt.Ignore());

            CreateMap<HelpRequest, HelpRequestUpdateDto>();
            CreateMap<HelpRequestUpdateDto, HelpRequest>()
                .ForMember(dest => dest.PostDate, opt => opt.Ignore())
                .ForMember(dest => dest.ExpireDate, opt => opt.Ignore());

            CreateMap<Gift, GiftDto>();

            CreateMap<GiftDto, Gift>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.PostDate, opt => opt.Ignore())
                .ForMember(dest => dest.ExpireDate, opt => opt.Ignore())
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.Attachment, opt => opt.Ignore());

            CreateMap<Gift, GiftUpdateDto>();
            CreateMap<GiftUpdateDto, Gift>()
                .ForMember(dest => dest.PostDate, opt => opt.Ignore())
                .ForMember(dest => dest.ExpireDate, opt => opt.Ignore())
                .ForMember(dest => dest.Attachment, opt => opt.Ignore());

            CreateMap<Report, ReportDto>();
            CreateMap<ReportDto, Report>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Timestamp, opt => opt.Ignore());

            CreateMap<Report, ReportUpdateDto>();
            CreateMap<ReportDto, Report>();

            CreateMap<Chat, ChatDto>();
            CreateMap<ChatDto, Chat>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Message, MessageDto>();
            CreateMap<MessageDto, Message>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDto, Notification>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<Notification, NotificationUpdateDto>();
            CreateMap<NotificationUpdateDto, Notification>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
