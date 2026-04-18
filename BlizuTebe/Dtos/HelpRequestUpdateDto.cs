using BlizuTebe.Models;

namespace BlizuTebe.Dtos
{
    public class HelpRequestUpdateDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public HelpCategory Category { get; set; }
        public string? Contact { get; set; }
        public HelpType HelpType { get; set; }
        public string? Attachment { get; set; }
        public HelpStatus Status { get; set; }
    }
}
