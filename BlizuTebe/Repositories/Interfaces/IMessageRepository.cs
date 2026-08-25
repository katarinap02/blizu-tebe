using BlizuTebe.Models;

namespace BlizuTebe.Repositories.Interfaces
{
    public interface IMessageRepository
    {
        void Create(Message message);
        void Update(Message message);
        void Delete(long id);
        List<Message> GetAllFromChat(long chatId);
    }
}
