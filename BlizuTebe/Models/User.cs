namespace BlizuTebe.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserRole Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Boolean IsVerified { get; set; }
        public long? LocalCommunityId { get; set; }
        public string? ProfilePicture { get; set; }
        public double Rating { get; set; }

        public User() { }

        public User(long id, string username, string password, string name, string surname, UserRole role, DateTime dateOfBirth, bool isVerified, long? localCommunityId, string profilePicture, double rating)
        {
            Id = id;
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            Role = role;
            DateOfBirth = dateOfBirth;
            IsVerified = isVerified;
            LocalCommunityId = localCommunityId;
            ProfilePicture = profilePicture;
            Rating = rating;
        }
    }
}

public enum UserRole { 
    Admin,
    Member
}
