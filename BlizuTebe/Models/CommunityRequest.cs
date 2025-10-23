namespace BlizuTebe.Models
{
    public class CommunityRequest
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? Picture { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Fulfilled { get; set; }
        public RequestType RequestType { get; set; }
        public long? AdminId { get; set; }
        public long? LocalCommunityId { get; set; }


        public CommunityRequest() { }

        public CommunityRequest(long id, string title, string description, string? picture, DateTime createdAt, bool fulfilled, RequestType request, long? adminId, long? localCommunityId)
        {
            Id = id;
            Title = title;
            Description = description;
            Picture = picture;
            CreatedAt = createdAt;
            Fulfilled = fulfilled;
            RequestType = request;
            AdminId = adminId;
            LocalCommunityId = localCommunityId;
        }
    }
}

public enum RequestType{
    Donation,
    Volunteering,
    Transport
}
