namespace BlizuTebe.Dtos
{
    public class AnnouncementDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? Picture { get; set; } // Nullable
        public string? ExistingPicture { get; set; }
        public DateTime PublishedAt { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsImportant { get; set; }
        public int? AdminId { get; set; }
        public int? LocalCommuntyId { get; set; }
    }
}
