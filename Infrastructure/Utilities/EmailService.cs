using Infrastructure.Configuration;
using Infrastructure.Contracts;
using Infrastructure.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Utilities
{
    public class EmailService : IEmailService
    {

        private readonly InfrastructureProperties _infrastructureProperties;
        private ILogger<EmailService> _logger;

        public EmailService(IOptions<InfrastructureProperties> infrastructureProperties, ILogger<EmailService> logger)
        {
            _infrastructureProperties = infrastructureProperties.Value;
            _logger = logger;
        }

        public async Task<(bool Success, string ErrorMessage)> SendEmailAsync(string to, string subject, string body)
        {
            try
            {

                using var smtpClient = new SmtpClient(_infrastructureProperties.SMTP_SERVER)
                {
                    Port = _infrastructureProperties.SMTP_PORT,
                    Credentials = new NetworkCredential(_infrastructureProperties.SMTP_USER, _infrastructureProperties.SMTP_PASSWORD),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_infrastructureProperties.SMTP_USER),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(to);

                await smtpClient.SendMailAsync(mailMessage);

                _logger.LogInformation("Email sent successfully to {Recipient}. Subject: {Subject}", to, subject);
                return (true, "");
            }
            catch (SmtpException smtpEx)
            {
                _logger.LogError(smtpEx, "SMTP exception while sending email to {Recipient}", to);
                return (false, $"SMTP error: {smtpEx.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "General exception while sending email to {Recipient}", to);
                return (false, $"General error: {ex.Message}");
            }
        }

    }
}
