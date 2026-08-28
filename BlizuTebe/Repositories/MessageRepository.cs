using BlizuTebe.Database;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;

namespace BlizuTebe.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDbContext _context;

        public MessageRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Create(Message message)
        {
            _context.Message.Add(message);
            _context.SaveChanges();
        }

        public void Update(Message message)
        {
            _context.Message.Update(message);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entity = _context.Message.Find(id);
            if (entity == null) return;

            _context.Message.Remove(entity);
            _context.SaveChanges();
        }

        public List<Message> GetAllFromChat(long chatId)
        {
            var messages = _context.Message.AsQueryable().Where(x => x.ChatId == chatId);
            return messages.ToList();
        }
    }
}
