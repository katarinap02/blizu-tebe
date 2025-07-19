using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void Create(User user);
        void Update(User user);
        void Delete(User user);
        User? GetById(long id);
        User? GetVerifiedByUsername(string username);
    }
}
