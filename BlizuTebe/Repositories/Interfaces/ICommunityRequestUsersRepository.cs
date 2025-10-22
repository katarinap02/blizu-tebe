using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface ICommunityRequestUsersRepository
    {
        void Create(CommunityRequestUsers communityRequestUsers);
        void Delete(CommunityRequestUsers communityRequestUsers);
        CommunityRequestUsers? GetByUserIdAndRequestId(long userId, long requestId);
        List<CommunityRequestUsers> GetAll();
        List<CommunityRequestUsers> GetByUserId(long userId);
        List<CommunityRequestUsers> GetByRequestId(long requestId);
    }
}
