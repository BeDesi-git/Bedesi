namespace BeDesi.Core.Models.Requests
{
    public class ManageBusinessRequest : RequestId
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public Business Business { get; set; }

    }

    public class GetUsersBusinessRequest : RequestId
    {
        public string Token { get; set; }
    }

    public class CheckBusinessNameRequest : RequestId
    {
        public string BusinessName { get; set; }
    }
}
