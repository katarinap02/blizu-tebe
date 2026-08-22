using BlizuTebe.Models;

namespace BlizuTebe.Dtos
{
    public class GiftUpdateDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
        public GiftCategory Category { get; set; }
        public string? Contact { get; set; }
        public IFormFile? Attachment { get; set; }
        public GiftStatus Status { get; set; }
    }
}
