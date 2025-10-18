namespace BlizuTebe.Models
{
    public class Announcement
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Picture { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsImportant { get; set; }
        public long? AdminId { get; set; }
        public long? LocalCommuntyId { get; set; }

        public Announcement() { }

        public Announcement(long id, string title, string description, string picture, DateTime publishedAt, DateTime expirationDate, bool isImportant, long? adminId, long? localCommuntyId)
        {
            Id = id;
            Title = title;
            Description = description;
            Picture = picture;
            PublishedAt = publishedAt;
            ExpirationDate = expirationDate;
            IsImportant = isImportant;
            AdminId = adminId;
            LocalCommuntyId = localCommuntyId;
        }
    }
}
