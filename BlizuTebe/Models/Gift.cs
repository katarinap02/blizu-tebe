namespace BlizuTebe.Models
{
    public class Gift
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public GiftCategory GiftCategory { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public GiftStatus Status { get; set; }
        public long UserId { get; set; }
        public string Contact {  get; set; }
        public string Attachment { get; set; }

        public Gift() { }

        public Gift(long id, string title, string description, GiftCategory category, DateTime postDate, DateTime expireDate, GiftStatus status, long userId, string contact, string attachment)
        {
            Id = id;
            Title = title;
            Description = description;
            GiftCategory = category;
            PostDate = postDate;
            ExpireDate = expireDate;
            Status = status;
            UserId = userId;
            Contact = contact;
            Attachment = attachment;
        }

    }

    public enum GiftCategory
    {
        Clothing,
        Electronics,
        Books,
        Toys,
        Furniture,
        Household,
        Sports,
        Other
    }

    public enum GiftStatus
    {
        Pending,
        Completed,
        Canceled,
        Expired
    }
}
