using BlizuTebe.Dtos;
using BlizuTebe.Models;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IMessageService
    {
        Result<MessageDto> Create(MessageDto message);
        Result<List<MessageDto>> GetAllFromChat(long chatId);
        Result<MessageDto> GetById(long id);
    }
}
