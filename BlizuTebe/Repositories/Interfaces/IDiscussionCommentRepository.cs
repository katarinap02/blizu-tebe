using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface IDiscussionCommentRepository
    {
        void Create(DiscussionComment an);
        void Update(DiscussionComment an);
        void Delete(DiscussionComment an);
        DiscussionComment? GetById(long id);
        List<DiscussionComment> GetAll();
        List<DiscussionComment> GetByDiscussionId(long discussionId);
    }
}
