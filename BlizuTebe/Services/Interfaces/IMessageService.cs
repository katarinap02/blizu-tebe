using BlizuTebe.Dtos;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IMessageService
    {
        Result<MessageDto> Create(MessageDto message);
        Result<List<MessageDto>> GetAllFromChat(long chatId);
    }
}
