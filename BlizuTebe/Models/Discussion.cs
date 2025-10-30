namespace BlizuTebe.Models
{
    public class Discussion
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isPinned { get; set; }
        public long? AdminId { get; set; }
        public long? LocalCommunityId { get; set; }

        public Discussion() { }

        public Discussion(long id, string name, string description, DateTime createdAt, bool isPinned, long? adminId, long? localCommunityId)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
            this.isPinned = isPinned;
            AdminId = adminId;
            LocalCommunityId = localCommunityId;
        }
    }
}
