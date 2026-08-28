using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface IChatRepository
    {
        void Create(Chat chat);
        void Update(Chat chat);
        void Delete(long id);
        List<Chat> GetAllForUser(long userId);
        Chat? GetById(long id);
        Chat? GetByUsers(long user1Id, long user2Id, long postId, PostType postType);
    }
}
