namespace BlizuTebe.Models
{
    public class DiscussionComment
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime CommentedAt { get; set; }
        public long UserId { get; set; }
        public long DiscussionId { get; set; }

        public DiscussionComment() { }

        public DiscussionComment(long id, string text, DateTime commentedAt, long userId, long discussionId)
        {
            Id = id;
            Text = text;
            CommentedAt = commentedAt;
            UserId = userId;
            DiscussionId = discussionId;
        }
    }
}
