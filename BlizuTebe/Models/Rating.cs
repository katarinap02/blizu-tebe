namespace BlizuTebe.Models
{
    public class Rating
    {
        public long Id { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime TimeStamp { get; set; }
        public long RaterId { get; set; }
        public long RatedId { get; set; }

        public Rating() { }

        public Rating(long id, int score, string? comment, DateTime timeStamp, long raterId, long ratedId)
        {
            Id = id;
            Score = score;
            Comment = comment;
            TimeStamp = timeStamp;
            RaterId = raterId;
            RatedId = ratedId;
        }
    }
}
