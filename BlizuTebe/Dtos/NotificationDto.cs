using BlizuTebe.Models;

namespace BlizuTebe.Dtos
{
    public class NotificationDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public NotificationType NotificationType { get; set; }
        public long UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
        public long RelatedObjectId { get; set; }
        public RelatedObjectType RelatedObjectType { get; set; }
    }
}
