using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface INotificationService
    {
        Result<NotificationDto> Create(NotificationDto notificationDto);
        Result<NotificationDto> Update(NotificationUpdateDto notificationUpdateDto);
        Result<List<NotificationDto>> GetByUser(long userId);
        Result<NotificationDto> GetById(long id);
    }
}
