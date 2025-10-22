namespace BlizuTebe.Models
{
    public class Discussion
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isClosed { get; set; }
        public long? AdminId { get; set; }
        public long? LocalCommuntyId { get; set; }

        public Discussion() { }

        public Discussion(long id, string name, string description, DateTime createdAt, bool isClosed, long? adminId, long? localCommuntyId)
        {
            Id = id;
            Name = name;
            Description = description;
            CreatedAt = createdAt;
            this.isClosed = isClosed;
            AdminId = adminId;
            LocalCommuntyId = localCommuntyId;
        }
    }
}
