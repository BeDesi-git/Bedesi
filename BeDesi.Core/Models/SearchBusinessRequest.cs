namespace BeDesi.Core.Models
{
    public class SearchBusinessRequest: RequestId
    {
        public string Keywords { get; set; }
        public string Location { get; set; }
    }

}
