using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface IDiscussionRepository
    {
        void Create(Discussion an);
        void Update(Discussion an);
        void Delete(Discussion an);
        Discussion? GetById(long id);
        List<Discussion> GetAll();
    }
}
