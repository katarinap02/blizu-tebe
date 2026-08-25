using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDbContext _context;

        public ChatRepository(AppDbContext context)
        {

            _context = context;
        }

        public void Create(Chat chat)
        {
            _context.Chat.Add(chat);
            _context.SaveChanges();
        }

        public void Update(Chat chat)
        {
            _context.Chat.Update(chat);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entity = _context.Chat.Find(id);
            if (entity == null) return;

            _context.Chat.Remove(entity);
            _context.SaveChanges();
        }

        public List<Chat> GetAllForUser(long userId)
        {
            var chats = _context.Chat.AsQueryable().Where(x => x.User1Id == userId || x.User2Id == userId);
            return chats.ToList();
        }

        public Chat GetById(long id)
        {
            return _context.Chat.Find(id);
        }

        public Chat? GetByUsers(long user1Id, long user2Id, long postId)
        {
            return _context.Chat.FirstOrDefault(x =>
                ((x.User1Id == user1Id && x.User2Id == user2Id) ||
                 (x.User1Id == user2Id && x.User2Id == user1Id)) &&
                x.PostId == postId);
        }

    }
}
