namespace BeDesi.Core.Models
{
    public class Business
    {
        public int BusinessId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string Description { get; set; }
        public string ContactNumber { get; set; }
        public string Website { get; set; }
        public string InstaHandle { get; set; }
        public string Facebook { get; set; }
        public string HasLogo { get; set; }
        public string ServesPostcode { get; set; } //comma seperated
        public string Keywords { get; set; } //comma seperated
        public List<BusinessRating>? Ratings { get; set; }
    }
}

