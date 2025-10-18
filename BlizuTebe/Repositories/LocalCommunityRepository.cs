using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class LocalCommunityRepository : ILocalCommunityRepository
    {
        private readonly AppDbContext _context;

        public LocalCommunityRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(LocalCommunity community)
        {
            _context.LocalCommunities.Add(community);
            _context.SaveChanges();
        }

        public void Delete(LocalCommunity community)
        {
            _context.LocalCommunities.Remove(community);
            _context.SaveChanges();
        }

        public LocalCommunity GetById(long id)
        {
            return _context.LocalCommunities.Find(id);
        }

        public List<LocalCommunity> GetAll()
        {
            return _context.LocalCommunities.ToList();
        }
    }
}
