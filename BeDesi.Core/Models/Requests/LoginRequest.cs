namespace BeDesi.Core.Models.Requests
{
    public class LoginRequest : RequestId
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class ForgotPasswordRequest : RequestId
    {
        public string Email { get; set; }
    }

    public class ResetPasswordRequest : RequestId
    {
        public string Token { get; set; }

        public string NewPassword { get; set; }
    }

}
