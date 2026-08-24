using BlizuTebe.Models;

namespace BlizuTebe.Dtos
{
    public class ReportDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public DateTime Timestamp { get; set; }
        public ReportStatus Status { get; set; }
        public long ReporterId { get; set; }
        public PostType PostType { get; set; }
        public long PostId { get; set; }
    }
}
