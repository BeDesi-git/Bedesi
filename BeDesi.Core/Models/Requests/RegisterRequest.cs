namespace BeDesi.Core.Models.Requests
{
    public class RegisterRequest : RequestId
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public bool IsBusinessOwner { get; set; }
        public bool IsAutoRegister { get; set; }
    }

    public class UpdateProfileRequest : RequestId
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }
        public bool IsBusinessOwner { get; set; }
    }

    public class GetProfileRequest : RequestId
    {
        public string Token { get; set; }
    }

}
