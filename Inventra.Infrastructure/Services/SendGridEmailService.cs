using Inventra.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Inventra.Infrastructure.Services
{
    public class SendGridEmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly string _fromEmail;
        private readonly string _fromName;

        public SendGridEmailService(IConfiguration configuration)
        {
            _apiKey = configuration["SendGrid:ApiKey"]!;
            _fromEmail = configuration["SendGrid:FromEmail"]!;
            _fromName = configuration["SendGrid:FromName"] ?? "Inventra";
        }

        public async Task SendEmailConfirmationAsync(string toEmail, string userName, string confirmationLink)
        {
            var client = new SendGridClient(_apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(_fromEmail, _fromName),
                Subject = "Confirm your Inventra account"
            };
            msg.AddTo(new EmailAddress(toEmail, userName));
            msg.HtmlContent = $"""
                <h2>Welcome to Inventra, {userName}!</h2>
                <p>Please confirm your email address by clicking the link below:</p>
                <p><a href="{confirmationLink}" style="background:#0097a7;color:white;padding:12px 24px;border-radius:6px;text-decoration:none;">Confirm Email</a></p>
                <p>If you didn't register, ignore this email.</p>
                """;
            msg.PlainTextContent = $"Confirm your email: {confirmationLink}";
            await client.SendEmailAsync(msg);
        }

        public async Task SendPasswordResetAsync(string toEmail, string userName, string resetLink)
        {
            var client = new SendGridClient(_apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(_fromEmail, _fromName),
                Subject = "Reset your Inventra password"
            };
            msg.AddTo(new EmailAddress(toEmail, userName));
            msg.HtmlContent = $"""
                <h2>Password Reset</h2>
                <p>Hi {userName}, click below to reset your password:</p>
                <p><a href="{resetLink}" style="background:#0097a7;color:white;padding:12px 24px;border-radius:6px;text-decoration:none;">Reset Password</a></p>
                <p>If you didn't request this, ignore this email.</p>
                """;
            msg.PlainTextContent = $"Reset your password: {resetLink}";
            await client.SendEmailAsync(msg);
        }
    }
}
