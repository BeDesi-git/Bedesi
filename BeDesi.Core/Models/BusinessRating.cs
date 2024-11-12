namespace BeDesi.Core.Models
{
    public class BusinessRating
    {
        public int Id { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public int Rating { get; set; }
        public string Feedback { get; set; }
    }
}

