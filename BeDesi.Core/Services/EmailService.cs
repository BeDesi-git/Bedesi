using BeDesi.Core.Models;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using BeDesi.Core.Services.Contracts;

namespace BeDesi.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            try
            {
                using var smtpClient = new SmtpClient(_emailSettings.SMTPHost, _emailSettings.SMTPPort)
                {
                    Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password),
                    EnableSsl = _emailSettings.EnableSSL
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mailMessage.To.Add(toEmail);

                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as required
                throw new InvalidOperationException("Failed to send email", ex);
            }
        }
    }
}
