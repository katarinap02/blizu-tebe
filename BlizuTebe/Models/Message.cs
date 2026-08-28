namespace BlizuTebe.Models
{
    public class Message
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public long ChatId { get; set; }

        public Message() { }

        public Message(long senderId, string content, DateTime timestamp, long chatId)
        {
            SenderId = senderId;
            Content = content;
            Timestamp = timestamp;
            ChatId = chatId;
        }
    }
}
