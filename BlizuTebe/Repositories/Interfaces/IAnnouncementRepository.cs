using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface IAnnouncementRepository
    {
        void Create(Announcement an);
        void Update(Announcement an);
        void Delete(Announcement an);
        Announcement? GetById(long id);
        List<Announcement> GetAll();
        bool ExistsByCommunityId(long communityId);
    }
}
