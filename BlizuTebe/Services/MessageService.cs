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
        private readonly INotificationService notificationService;
        private readonly IChatService chatService;

        public MessageService(IMapper mapper, IMessageRepository messageRepository, INotificationService notificationService, IChatService chatService)
        {
            _mapper = mapper;
            _messageRepository = messageRepository;
            this.notificationService = notificationService;
            this.chatService = chatService;
        }

        public Result<MessageDto> Create(MessageDto messageDto)
        {
            var newMessage = _mapper.Map<Message>(messageDto);

            _messageRepository.Create(newMessage);

            var chat = chatService.GetById(newMessage.ChatId);

            if (chat.IsFailed)
                return Result.Fail<MessageDto>(chat.Errors);

            long receiverId;
            if (chat.Value.User1Id == newMessage.SenderId)
                receiverId = chat.Value.User2Id;
            else if (chat.Value.User2Id == newMessage.SenderId)
                receiverId = chat.Value.User1Id;
            else
                return Result.Fail<MessageDto>("Sender is not a member of this chat.");

            var reporterNotification = new Notification("Dobili ste novu poruku", NotificationType.NewMessage, receiverId, DateTime.UtcNow, newMessage.Id, RelatedObjectType.Message);
            notificationService.Create(_mapper.Map<NotificationDto>(reporterNotification));
            
            return Result.Ok(_mapper.Map<MessageDto>(newMessage));
        }

        public Result<List<MessageDto>> GetAllFromChat(long chatId)
        {
            var messages = _messageRepository.GetAllFromChat(chatId);

            return Result.Ok(_mapper.Map<List<MessageDto>>(messages));
        }

        public Result<MessageDto> GetById(long id)
        {
            var message = _messageRepository.GetById(id);
            if (message == null) return Result.Fail<MessageDto>("Message not found with id: " + id);

            return Result.Ok(_mapper.Map<MessageDto>(message));
        }
    }
}
