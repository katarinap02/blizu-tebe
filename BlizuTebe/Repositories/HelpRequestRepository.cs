using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class HelpRequestRepository : IHelpRequestRepository
    {
        private readonly AppDbContext _context;

        public HelpRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(HelpRequest helpRequest)
        {
            _context.HelpRequests.Add(helpRequest);
            _context.SaveChanges();
        }

        public void Update(HelpRequest helpRequest)
        {
            _context.HelpRequests.Update(helpRequest);
            _context.SaveChanges();
        }

        public void Delete(long helpRequestId)
        {
            var entity = _context.HelpRequests.Find(helpRequestId);
            if (entity == null) return;

            _context.HelpRequests.Remove(entity);
            _context.SaveChanges();
        }

        public List<HelpRequest> GetAll()
        {
            return _context.HelpRequests.ToList();
        }

        public HelpRequest GetById(long id)
        {
            return _context.HelpRequests.Find(id);
        }
    }
}
