namespace BeDesi.Core.Models
{
    public class User
    {
        public int UserId {  get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string PasswordHash { get; set; }
        public string Salt { get; set; }
        public string Role { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool isActive { get; set; }
    }

    public class AuthenticatedUser
    {
        public string Token { get; set; }
        public User UserDetails { get; set; }
    }
}

