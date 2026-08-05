namespace BlizuTebe.Models
{
    public class Report
    {
        public long Id;
        public string Description;
        public DateTime Timestamp;
        public ReportStatus Status;
    }

    public enum ReportStatus
    {
        PENDING,
        REJECTED,
        ACCEPTED
    }
}
