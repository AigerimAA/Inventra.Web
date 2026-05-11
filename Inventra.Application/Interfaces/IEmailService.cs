namespace Inventra.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailConfirmationAsync(string toEmail, string userName, string confirmationLink);
        Task SendPasswordResetAsync(string toEmail, string userName, string resetLink);
    }
}
