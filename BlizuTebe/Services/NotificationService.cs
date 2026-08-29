using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IMapper _mapper;
        private readonly INotificationRepository notificationRepository;

        public NotificationService(IMapper mapper, INotificationRepository notificationRepository)
        {
            _mapper = mapper;
            this.notificationRepository = notificationRepository;
        }

        public Result<NotificationDto> Create(NotificationDto notificationDto)
        {
            var newNotification = _mapper.Map<Notification>(notificationDto);
            notificationRepository.Create(newNotification);
            return Result.Ok(_mapper.Map<NotificationDto>(newNotification));
        }

        public Result<NotificationDto> Update(NotificationUpdateDto notificationDto)
        {
            var notification = notificationRepository.GetById(notificationDto.Id);
            if(notification == null) return Result.Fail<NotificationDto>("Notification not found with id: " +  notificationDto.Id);

            _mapper.Map(notificationDto, notification);
            notificationRepository.Update(notification);
            return Result.Ok(_mapper.Map<NotificationDto>(notification));
        }

        public Result<List<NotificationDto>> GetByUser(long userId)
        {
            var notifications = notificationRepository.GetByUser(userId);
            return Result.Ok(_mapper.Map<List<NotificationDto>>(notifications));
        }

        public Result<NotificationDto> GetById(long id)
        {
            var notification = notificationRepository.GetById(id);
            if (notification == null) return Result.Fail<NotificationDto>("Notification not found with id: " + id);

            return Result.Ok(_mapper.Map<NotificationDto>(notification));
        }
    }
}
