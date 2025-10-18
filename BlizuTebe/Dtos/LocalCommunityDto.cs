namespace BlizuTebe.Dtos
{
    public class LocalCommunityDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Boundary { get; set; } 
        public double[] CenterPoint { get; set; } 
        public int? PresidentId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Facebook { get; set; }
    }
}
