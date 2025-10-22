using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class CommunityRequestRepository : ICommunityRequestRepository
    {
        private readonly AppDbContext _context;

        public CommunityRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(CommunityRequest request)
        {
            _context.CommunityRequests.Add(request);
            _context.SaveChanges();
        }

        public void Delete(CommunityRequest request)
        {
            _context.CommunityRequests.Remove(request);
            _context.SaveChanges();
        }

        public List<CommunityRequest> GetAll()
        {
            return _context.CommunityRequests.ToList();
        }

        public CommunityRequest? GetById(long id)
        {
            return _context.CommunityRequests.FirstOrDefault(r => r.Id == id);
        }

        public void Update(CommunityRequest request)
        {
            _context.CommunityRequests.Update(request);
            _context.SaveChanges();
        }
    }
}
