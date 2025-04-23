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

        public async Task SendWelcomeEmailAsync(string userName, string businessName, string userEmail, string password)
        {
            string subject = "Welcome to Bedesi.co.uk!";
            string htmlBody = @"
                <!DOCTYPE html>
                <html>
                <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0;'>
                    <div style='width: 100%; max-width: 600px; margin: 20px auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #f9f9f9;'>
                        <h2 style='color: #4E68AF; margin-top: 0;'>Welcome to Bedesi.co.uk!</h2>
                        <p>Dear <strong>[User's Full Name]</strong>,</p>
                        <p>Thank you for registering your business, <strong>[Business Name]</strong>, on <strong>Bedesi.co.uk</strong>! We’re excited to have you on board and help connect your business with local customers.</p>
                        <p>Here are your account credentials for accessing and managing your business profile:</p>
                        <p><strong>Username:</strong> [User's Username]</p>
                        <p><strong>Password:</strong> [User's Password]</p>
                        <p>For your security, we recommend changing your password after your first login.</p>
                        <p>If you need any assistance, feel free to reach out to our support team at <a href='mailto:support@bedesi.co.uk' style='color: #4E68AF; text-decoration: none;'>support@bedesi.co.uk</a>.</p>
                        
                        <p>Best regards,</p>
                        <p><strong>Bedesi.co.uk</strong></p>
                    </div>
                </body>
                </html>";

            // Replace placeholders in HTML body dynamically
            htmlBody = htmlBody.Replace("[User's Full Name]", userName)
                               .Replace("[Business Name]", businessName)
                               .Replace("[User's Username]", userEmail)
                               .Replace("[User's Password]", password);

             await SendEmailAsync(userEmail, subject, htmlBody);
        }
        public async Task SendWelcomeEmailAsync(string userName, string userEmail, string password)
        {
            string subject = "Welcome to Bedesi.co.uk!";
            string htmlBody = @"
                <!DOCTYPE html>
                <html>
                <body style='font-family: Arial, sans-serif; line-height: 1.6; color: #333; margin: 0; padding: 0;'>
                    <div style='width: 100%; max-width: 600px; margin: 20px auto; padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #f9f9f9;'>
                        <h2 style='color: #4E68AF; margin-top: 0;'>Welcome to Bedesi.co.uk!</h2>
                        <p>Dear <strong>[User's Full Name]</strong>,</p>
                        
                        <p>Here are your account credentials for accessing and managing your profile:</p>
                        <p><strong>Username:</strong> [User's Username]</p>
                        <p><strong>Password:</strong> [User's Password]</p>
                        <p>For your security, we recommend changing your password after your first login.</p>
                        <p>If you need any assistance, feel free to reach out to our support team at <a href='mailto:support@bedesi.co.uk' style='color: #4E68AF; text-decoration: none;'>support@bedesi.co.uk</a>.</p>
                        <p>We’re here to help you succeed!</p>
                        <p>Best regards,</p>
                        <p><strong>Bedesi.co.uk</strong></p>
                    </div>
                </body>
                </html>";

            // Replace placeholders in HTML body dynamically
            htmlBody = htmlBody.Replace("[User's Full Name]", userName)
                               .Replace("[User's Username]", userEmail)
                               .Replace("[User's Password]", password);

            await SendEmailAsync(userEmail, subject, htmlBody);
        }

    }
}
