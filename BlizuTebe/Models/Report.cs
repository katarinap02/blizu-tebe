using Npgsql.PostgresTypes;

namespace BlizuTebe.Models
{
    public class Report
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public ReportStatus Status { get; set; }
        public long ReporterId { get; set; }
        public PostType PostType { get; set; }
        public long PostId { get; set; }

        public Report() { }

        public Report(long id, string description, DateTime timestamp, ReportStatus status, long reporterId, PostType postType, long postId)
        {
            Id = id;
            Description = description;
            Timestamp = timestamp;
            Status = status;
            ReporterId = reporterId;
            PostType = postType;
            PostId = postId;
        }
    }

    public enum ReportStatus
    {
        Pending,
        Rejected,
        Accepted
    }

    public enum PostType
    {
        HelpRequest,
        Gift
    }
}
