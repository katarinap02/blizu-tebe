namespace BlizuTebe.Dtos
{
    public class AnnouncementDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Picture { get; set; }
        public string? ExistingPicture { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsImportant { get; set; }
        public long? AdminId { get; set; }
        public long? LocalCommuntyId { get; set; }
    }
}
