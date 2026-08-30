using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        void Create(Notification notification);
        void Update(Notification notification);
        List<Notification> GetByUser(long userId);
        Notification GetById(long id);
    }
}
