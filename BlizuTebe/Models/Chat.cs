namespace BlizuTebe.Models
{
    public class Chat
    {
        public long Id { get; set; }
        public long User1Id { get; set; }
        public long User2Id { get; set; }
        public long PostId { get; set; }
        public PostType PostType { get; set; }

        public Chat() { }
        
        public Chat(long user1Id, long user2Id, long postId, PostType postType)
        {
            User1Id = user1Id;
            User2Id = user2Id;
            PostId = postId;
            PostType = postType;
        }
    }
}
