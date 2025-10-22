namespace BlizuTebe.Dtos
{
    public class CommunityRequestDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile? FilePicture { get; set; }
        public string? Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Fulfilled { get; set; }
        public RequestType RequestType { get; set; }
        public long? AdminId { get; set; }
        public long? LocalCommuntyId { get; set; }
    }
}
