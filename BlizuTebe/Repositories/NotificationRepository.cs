using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Notification notification)
        {
            _context.Notifications.Add(notification);
            _context.SaveChanges();
        }

        public void Update(Notification notification)
        {
            _context.Notifications.Update(notification);
            _context.SaveChanges();
        }

        public List<Notification> GetByUser(long userId)
        {
            return _context.Notifications.Where(x => x.UserId == userId).OrderByDescending(x => x.Timestamp).ToList();
        }

        public Notification GetById(long id)
        {
            return _context.Notifications.Find(id);
        }
    }
}
