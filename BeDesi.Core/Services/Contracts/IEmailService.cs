namespace BeDesi.Core.Services.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
        Task SendWelcomeEmailAsync(string userName, string businessName, string businessEmail, string password);
        Task SendWelcomeEmailAsync(string userName, string userEmail, string password);
    }
}
