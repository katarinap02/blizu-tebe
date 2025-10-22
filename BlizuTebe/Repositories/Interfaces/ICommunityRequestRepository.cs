using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface ICommunityRequestRepository
    {
        void Create(CommunityRequest an);
        void Update(CommunityRequest an);
        void Delete(CommunityRequest an);
        CommunityRequest? GetById(long id);
        List<CommunityRequest> GetAll();
    }
}
