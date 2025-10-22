using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class DiscussionCommentRepository : IDiscussionCommentRepository
    {
        private readonly AppDbContext _context;

        public DiscussionCommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(DiscussionComment comment)
        {
            _context.DiscussionComments.Add(comment);
            _context.SaveChanges();
        }

        public void Delete(DiscussionComment comment)
        {
            _context.DiscussionComments.Remove(comment);
            _context.SaveChanges();
        }

        public List<DiscussionComment> GetAll()
        {
            return _context.DiscussionComments.ToList();
        }

        public List<DiscussionComment> GetByDiscussionId(long discussionId)
        {
            return _context.DiscussionComments
                   .Where(c => c.DiscussionId == discussionId)
                   .OrderBy(c => c.CommentedAt) 
                   .ToList();
        }

        public DiscussionComment? GetById(long id)
        {
            return _context.DiscussionComments.FirstOrDefault(c => c.Id == id);
        }

        public void Update(DiscussionComment comment)
        {
            _context.DiscussionComments.Update(comment);
            _context.SaveChanges();
        }
    }
}
