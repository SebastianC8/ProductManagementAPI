using Infrastructure.Configuration;
using Infrastructure.Contracts;
using Infrastructure.Data;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Infrastructure.Utilities
{
    public class EmailService : IEmailService
    {

        private readonly InfrastructureProperties _infrastructureProperties;

        public EmailService(IOptions<InfrastructureProperties> infrastructureProperties)
        {
            _infrastructureProperties = infrastructureProperties.Value;
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

                return (true, ""); // Retorna éxito
            }
            catch (SmtpException smtpEx)
            {
                // Aquí puedes manejar errores de SMTP de forma más específica
                return (false, $"SMTP error: {smtpEx.Message}");
            }
            catch (Exception ex)
            {
                // Captura errores generales
                return (false, $"General error: {ex.Message}");
            }
        }

    }
}
