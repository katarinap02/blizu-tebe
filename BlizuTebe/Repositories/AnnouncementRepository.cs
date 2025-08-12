using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class AnnouncementRepository : IAnnouncementRepository
    {
        private readonly AppDbContext _context;

        public AnnouncementRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Create(Announcement an)
        {
            _context.Announcements.Add(an);
            _context.SaveChanges();
        }

        public void Delete(Announcement an)
        {
            _context.Announcements.Remove(an);
            _context.SaveChanges();
        }

        public List<Announcement> GetAll()
        {
            return _context.Announcements.ToList();
        }

        public Announcement? GetById(long id)
        {
            return _context.Announcements.FirstOrDefault(a => a.Id == id);
        }

        public void Update(Announcement an)
        {
            _context.Announcements.Update(an);
            _context.SaveChanges();
        }
    }
}
