namespace BlizuTebe.Dtos
{
    public class DiscussionDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isPinned { get; set; }
        public long? AdminId { get; set; }
        public long? LocalCommunityId { get; set; }
    }
}
