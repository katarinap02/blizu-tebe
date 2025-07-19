namespace BlizuTebe.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public UserRole Role { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Boolean IsVerified { get; set; }

        public User(int id, string username, string password, string name, string surname, UserRole role, DateTime dateOfBirth, bool isVerified)
        {
            Id = id;
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            this.Role = role;
            this.DateOfBirth = dateOfBirth;
            this.IsVerified = isVerified;
        }

    }
}

public enum UserRole { 
    Admin,
    Member
}
