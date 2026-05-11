using Inventra.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace Inventra.Infrastructure.Services
{
    public class BrevoEmailService : IEmailService
    {
        private readonly string _login;
        private readonly string _smtpPassword;
        private readonly string _fromEmail;
        private readonly ILogger<BrevoEmailService> _logger;

        public BrevoEmailService(IConfiguration configuration, ILogger<BrevoEmailService> logger)
        {
            _login = configuration["Brevo:Login"]!;
            _smtpPassword = configuration["Brevo:SmtpPassword"]!;
            _fromEmail = configuration["Brevo:FromEmail"]!;
            _logger = logger;
        }

        public async Task SendEmailConfirmationAsync(string toEmail, string userName, string confirmationLink)
        {
            try
            {
                _logger.LogInformation("Sending confirmation email to {Email}", toEmail);
                using var client = new SmtpClient("smtp-relay.brevo.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_login, _smtpPassword)
                };
                var message = new MailMessage
                {
                    From = new MailAddress(_fromEmail, "Inventra"),
                    Subject = "Confirm your Inventra account",
                    IsBodyHtml = true,
                    Body = $"""
                    <div style="font-family: Arial, sans-serif; max-width: 500px; margin: 0 auto; padding: 20px;">
                        <h2 style="color: #0097a7;">Welcome to Inventra, {userName}!</h2>
                        <p style="font-size: 16px; color: #333;">Thank you for registering. Please confirm your email address to get started.</p>
                        <p style="font-size: 14px; color: #666; margin-bottom: 20px;">Click the button below to confirm your email:</p>
                        <a href="{confirmationLink}" style="display: inline-block; background: #0097a7; color: white; padding: 12px 30px; border-radius: 6px; text-decoration: none; font-size: 16px;">Confirm Email</a>
                        <p style="font-size: 12px; color: #999; margin-top: 20px;">If you didn't register on Inventra, you can safely ignore this email.</p>
                    </div>
                    """
                };
                message.To.Add(new MailAddress(toEmail, userName));
                await client.SendMailAsync(message);
                _logger.LogInformation("Email sent successfully to {Email}", toEmail);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send confirmation email to {Email}", toEmail);
            }
        }

        public async Task SendPasswordResetAsync(string toEmail, string userName, string resetLink)
        {
            try
            {
                using var client = new SmtpClient("smtp-relay.brevo.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(_login, _smtpPassword)
                };
                var message = new MailMessage
                {
                    From = new MailAddress(_fromEmail, "Inventra"),
                    Subject = "Reset your Inventra password",
                    IsBodyHtml = true,
                    Body = $"""
                    <div style="font-family: Arial, sans-serif; max-width: 500px; margin: 0 auto; padding: 20px;">
                        <h2 style="color: #0097a7;">Password Reset</h2>
                        <p style="font-size: 16px; color: #333;">Hi {userName}, click below to reset your password:</p>
                        <a href="{resetLink}" style="display: inline-block; background: #0097a7; color: white; padding: 12px 30px; border-radius: 6px; text-decoration: none; font-size: 16px;">Reset Password</a>
                        <p style="font-size: 12px; color: #999; margin-top: 20px;">If you didn't request this, ignore this email.</p>
                    </div>
                    """
                };
                message.To.Add(new MailAddress(toEmail, userName));
                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send password reset email to {Email}", toEmail);
            }
        }
    }
}
