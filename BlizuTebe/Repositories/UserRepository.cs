using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User? GetById(long id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User? GetVerifiedByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.IsVerified);
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public bool ExistsByCommunityId(long communityId)
        {
            try { 
            return _context.Users.Any(u => u.LocalCommunityId == communityId);
            }
            catch
            {
                return false; 
            }
        }

    }
}
