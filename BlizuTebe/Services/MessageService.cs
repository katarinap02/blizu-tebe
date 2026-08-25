using AutoMapper;
using BlizuTebe.Dtos;
using BlizuTebe.Models;
using BlizuTebe.Repositories.Interfaces;
using BlizuTebe.Services.Interfaces;
using FluentResults;

namespace BlizuTebe.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMapper _mapper;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMapper mapper, IMessageRepository messageRepository)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
        }

        public Result<MessageDto> Create(MessageDto messageDto)
        {
            var newMessage = _mapper.Map<Message>(messageDto);

            _messageRepository.Create(newMessage);

            return Result.Ok(_mapper.Map<MessageDto>(newMessage));
        }

        public Result<List<MessageDto>> GetAllFromChat(long chatId)
        {
            var messages = _messageRepository.GetAllFromChat(chatId);

            return Result.Ok(_mapper.Map<List<MessageDto>>(messages));
        }
    }
}
