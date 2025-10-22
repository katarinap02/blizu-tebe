using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class CommunityRequestUsersRepository : ICommunityRequestUsersRepository
    {
        private readonly AppDbContext _context;

        public CommunityRequestUsersRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(CommunityRequestUsers communityRequestUsers)
        {
            _context.CommunityRequestUsers.Add(communityRequestUsers);
            _context.SaveChanges();
        }

        public void Delete(CommunityRequestUsers communityRequestUsers)
        {
            _context.CommunityRequestUsers.Remove(communityRequestUsers);
            _context.SaveChanges();
        }

        public CommunityRequestUsers? GetByUserIdAndRequestId(long userId, long requestId)
        {
            return _context.CommunityRequestUsers
                .FirstOrDefault(cru => cru.UserId == userId && cru.CommunityRequestId == requestId);
        }

        public List<CommunityRequestUsers> GetAll()
        {
            return _context.CommunityRequestUsers.ToList();
        }

        public List<CommunityRequestUsers> GetByUserId(long userId)
        {
            return _context.CommunityRequestUsers
                .Where(cru => cru.UserId == userId)
                .ToList();
        }

        public List<CommunityRequestUsers> GetByRequestId(long requestId)
        {
            return _context.CommunityRequestUsers
                .Where(cru => cru.CommunityRequestId == requestId)
                .ToList();
        }
    }
}
