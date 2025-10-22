namespace BlizuTebe.Dtos
{
    public class RatingDto
    {
        public long Id { get; set; }
        public int Score { get; set; }
        public string? Comment { get; set; }
        public DateTime TimeStamp { get; set; }
        public long RaterId { get; set; }
        public long RatedId { get; set; }
    }
}
