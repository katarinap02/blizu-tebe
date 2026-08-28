using BlizuTebe.Models;

namespace BlizuTebe.Dtos
{
    public class ChatDto
    {
        public long Id { get; set; }
        public long User1Id { get; set; }
        public long User2Id { get; set; }
        public long PostId { get; set; }
        public PostType PostType { get; set; }
    }
}
