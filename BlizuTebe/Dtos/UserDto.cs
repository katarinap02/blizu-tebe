namespace BlizuTebe.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserRole Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Boolean IsVerified { get; set; }
        public int? LocalCommunityId { get; set; }
        public IFormFile? Picture { get; set; }
        public string? ProfilePicture { get; set; }
        public double Rating { get; set; }

    }
}
