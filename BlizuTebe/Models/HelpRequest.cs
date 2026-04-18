using System.ComponentModel.Design;

namespace BlizuTebe.Models
{
    public class HelpRequest
    {
        public long Id {  get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public HelpCategory Category { get; set; }
        public DateTime PostDate { get; set; }
        public DateTime ExpireDate { get; set; }
        public HelpStatus Status {  get; set; }
        public long UserId { get; set; }
        public string Contact {  get; set; }
        public HelpType HelpType { get; set; }
        public string? Attachment {  get; set; }


        public HelpRequest() { }
        public HelpRequest(long id, string title, string description, HelpCategory category, DateTime postDate, DateTime expireDate, HelpStatus status, long userId, string contact, HelpType helpType, string? attachment)
        {
            Id = id;
            Title = title;
            Description = description;
            Category = category;
            PostDate = postDate;
            ExpireDate = expireDate;
            Status = status;
            UserId = userId;
            Contact = contact;
            HelpType = helpType;
            Attachment = attachment;
        }
    }
    public enum HelpCategory
    {
        OldPeopleHelp,
        HouseKeeping,
        PetCare,
        SmallRepairs,
        StudyHelp,
        ThingsExchange,
        PhysicalWork,
        Socializing
    }

    public enum HelpStatus
    {
        Pending,
        Completed,
        Canceled,
        Expired
    }

    public enum HelpType
    {
        Asking,
        Offering
    }


}
