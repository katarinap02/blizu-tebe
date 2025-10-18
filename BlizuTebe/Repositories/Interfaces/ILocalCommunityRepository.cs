using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface ILocalCommunityRepository
    {
        void Create(LocalCommunity community);
        void Delete(LocalCommunity community);
        LocalCommunity GetById(long id);
        List<LocalCommunity> GetAll();
    }
}
