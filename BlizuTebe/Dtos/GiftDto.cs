using BlizuTebe.Models;

namespace BlizuTebe.Dtos
{
    public class GiftDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public GiftCategory GiftCategory { get; set; }
        public GiftStatus Status { get; set; }
        public long UserId { get; set; }
        public string Contact {  get; set; }
        public string Attachment { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
