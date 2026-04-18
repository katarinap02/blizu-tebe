using BlizuTebe.Models;

namespace BlizuTebe.Dtos
{
    public class HelpRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public HelpCategory Category { get; set; }
        public HelpStatus Status { get; set; }
        public long UserId { get; set; }
        public string Contact { get; set; }
        public HelpType HelpType { get; set; }
        public string? Attachment { get; set; }
    }
}
