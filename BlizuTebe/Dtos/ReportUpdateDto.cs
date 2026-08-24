using BlizuTebe.Models;

namespace BlizuTebe.Dtos
{
    public class ReportUpdateDto
    {
        public long Id {  get; set; }
        public ReportStatus Status { get; set; }
    }
}
