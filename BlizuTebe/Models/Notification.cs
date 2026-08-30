namespace BlizuTebe.Models
{
    public class Notification
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public NotificationType NotificationType { get; set; }
        public long UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; } = false;
        public long RelatedObjectId { get; set; }
        public RelatedObjectType RelatedObjectType{ get; set; }

        public Notification() { }

        public Notification(string description, NotificationType notificationType, long userId, DateTime timestamp, long relatedObjectId, RelatedObjectType relatedObjectType)
        {
            Description = description;
            NotificationType = notificationType;
            UserId = userId;
            Timestamp = timestamp;
            RelatedObjectId = relatedObjectId;
            RelatedObjectType = relatedObjectType;
        }
    }

    public enum NotificationType
    {
        NewMessage,
        NewReport,
        ReportAccepted,
        ReportRejected,
        HelpRequestMatched
    }

    public enum RelatedObjectType
    {
        Message,
        Gift,
        HelpRequest
    }
}
