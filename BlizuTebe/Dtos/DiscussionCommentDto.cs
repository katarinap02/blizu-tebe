namespace BlizuTebe.Dtos
{
    public class DiscussionCommentDto
    {
        public long Id { get; set; }
        public string Text { get; set; }
        public DateTime CommentedAt { get; set; }
        public long UserId { get; set; }
        public long DiscussionId { get; set; }
    }
}
