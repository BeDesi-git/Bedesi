namespace BeDesi.Core.Models
{
    public class EmailSettings
    {
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSSL { get; set; }
    }
}

