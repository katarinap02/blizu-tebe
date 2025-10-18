namespace BlizuTebe.Dtos
{
    public class LocalCommunityDto
    {
        public long? Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Boundary { get; set; } 
        public double[] CenterPoint { get; set; } 
        public long? PresidentId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Facebook { get; set; }
    }
}
