namespace Infrastructure.Contracts
{
    public interface IEmailService
    {
        Task<(bool Success, string ErrorMessage)> SendEmailAsync(string to, string subject, string body);
    }
}
