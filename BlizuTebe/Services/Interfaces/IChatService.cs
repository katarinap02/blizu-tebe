using BlizuTebe.Dtos;
using BlizuTebe.Models;
using FluentResults;

namespace BlizuTebe.Services.Interfaces
{
    public interface IChatService
    {
        Result<ChatDto> Create(ChatDto chatDto);
        Result<ChatDto> Update(ChatDto chatDto);
        Result<ChatDto> Delete(long chatId);
        Result<ChatDto> GetById(long chatId);
        Result<List<ChatDto>> GetAllForUser(long userId);
        Result<ChatDto> GetByUsers(long user1Id, long user2Id, long postId, PostType postType);
        Result<ChatDto> GetOrCreate(long user1Id, long user2Id, long postId, PostType postType);
    }
}
