namespace BlizuTebe.Models
{
    public class Announcement
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime ExpirationDate { get; set; }

        public Announcement() { }

        public Announcement(int id, string title, string description, string picture, DateTime publishedAt, DateTime expirationDate)
        {
            Id = id;
            Title = title;
            Description = description;
            Picture = picture;
            PublishedAt = publishedAt;
            ExpirationDate = expirationDate;
        }
    }
}
