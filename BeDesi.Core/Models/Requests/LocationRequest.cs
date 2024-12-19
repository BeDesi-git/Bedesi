namespace BeDesi.Core.Models.Requests
{
    public class LocationRequest : RequestId
    {
        public string StartsWith { get; set; }
    }

    public class AllLocationRequest : RequestId
    {
    }

}
