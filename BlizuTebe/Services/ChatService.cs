using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class ChatService : IChatService
    {
        private readonly IMapper _mapper;
        private readonly IChatRepository chatRepository;

        public ChatService(IMapper mapper, IChatRepository chatRepository)
        {
            _mapper = mapper;
            this.chatRepository = chatRepository;
        }

        public Result<ChatDto> Create(ChatDto chatDto)
        {
            var newChat = _mapper.Map<Chat>(chatDto);
            chatRepository.Create(newChat);
            return Result.Ok(_mapper.Map<ChatDto>(newChat));
        }

        public Result<ChatDto> Update(ChatDto chatDto)
        {
            var chat = chatRepository.GetById(chatDto.Id);
            if (chat == null) return Result.Fail<ChatDto>("Chat not found with ID: " + chatDto.Id);

            _mapper.Map(chatDto, chat);
            chatRepository.Update(chat);
            return Result.Ok(_mapper.Map<ChatDto>(chat));
        }

        public Result<ChatDto> Delete(long id)
        {
            var chat = chatRepository.GetById(id);
            if (chat == null) return Result.Fail<ChatDto>("Chat not found with ID: " + id);

            chatRepository.Delete(id);
            return Result.Ok(_mapper.Map<ChatDto>(chat));
        }

        public Result<List<ChatDto>> GetAllForUser(long userId)
        {
            var chats = chatRepository.GetAllForUser(userId);
            return Result.Ok(_mapper.Map<List<ChatDto>>(chats));
        }

        public Result<ChatDto> GetById(long id)
        {
            var chat = chatRepository.GetById(id);
            if (chat == null) return Result.Fail<ChatDto>("Chat not found with ID: " + id);

            return Result.Ok(_mapper.Map<ChatDto>(chat));
        }

        public Result<ChatDto> GetByUsers(long user1Id, long user2Id, long postId)
        {
            var chat = chatRepository.GetByUsers(user1Id, user2Id, postId);

            if (chat == null)
                return Result.Fail<ChatDto>("Chat not found.");

            return Result.Ok(_mapper.Map<ChatDto>(chat));
        }

        public Result<ChatDto> GetOrCreate(long user1Id, long user2Id, long postId)
        {
            var chat = chatRepository.GetByUsers(user1Id, user2Id, postId);

            if (chat != null)
                return Result.Ok(_mapper.Map<ChatDto>(chat));

            var newChat = new Chat
            {
                User1Id = user1Id,
                User2Id = user2Id,
                PostId = postId
            };

            chatRepository.Create(newChat);

            return Result.Ok(_mapper.Map<ChatDto>(newChat));
        }
    }
}
