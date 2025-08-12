namespace BlizuTebe.Dtos
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Picture { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
