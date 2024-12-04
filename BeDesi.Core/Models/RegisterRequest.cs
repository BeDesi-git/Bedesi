﻿namespace BeDesi.Core.Models
{
    public class RegisterRequest : RequestId
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class UpdateProfileRequest : RequestId
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string ContactNumber { get; set; }
        public string Password { get; set; }
    }

    public class GetProfileRequest : RequestId
    {
        public string Token { get; set; }
    }

}
