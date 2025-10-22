using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class DiscussionRepository : IDiscussionRepository
    {
        private readonly AppDbContext _context;

        public DiscussionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Discussion discussion)
        {
            _context.Discussions.Add(discussion);
            _context.SaveChanges();
        }

        public void Delete(Discussion discussion)
        {
            _context.Discussions.Remove(discussion);
            _context.SaveChanges();
        }

        public List<Discussion> GetAll()
        {
            return _context.Discussions.ToList();
        }

        public Discussion? GetById(long id)
        {
            return _context.Discussions.FirstOrDefault(d => d.Id == id);
        }

        public void Update(Discussion discussion)
        {
            _context.Discussions.Update(discussion);
            _context.SaveChanges();
        }
    }
}
