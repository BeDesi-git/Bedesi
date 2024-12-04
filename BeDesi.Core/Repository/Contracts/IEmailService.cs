namespace BeDesi.Core.Repository.Contracts
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
