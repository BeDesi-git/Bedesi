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
        public string Email { get; set; }
        public string Website { get; set; }
        public string InstaHandle { get; set; }
        public string Facebook { get; set; }
        public bool HasLogo { get; set; }
        public List<string> ServesPostcodes { get; set; } //comma seperated
        public List<string> Keywords { get; set; } //comma seperated
        public int Points { get; set; }
        public int OwnerId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        public Business()
        {
            ServesPostcodes = new List<string>();
            Keywords = new List<string>();
            HasLogo = false;
            IsActive = false;
        }
    }
}

