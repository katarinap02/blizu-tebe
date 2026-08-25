namespace BlizuTebe.Dtos
{
    public class MessageDto
    {
        public long Id { get; set; }
        public long SenderId { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public long ChatId { get; set; }
    }
}
