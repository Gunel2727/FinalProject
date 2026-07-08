using Microsoft.Extensions.Options;
using SIS.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SIS.Infrastructure.Identity
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendPasswordResetEmailAsync(string toEmail, string resetToken)
        {
            var resetLink = $"http://localhost:5500/reset-password.html?email={toEmail}&token={resetToken}";

            var message = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = "Reset Password — Student Information System",
                Body = $@"
                <h2>Password Reset Request</h2>
                <p>To reset your password, please click the link below:</p>
                <p><a href='{resetLink}'>{resetLink}</a></p>
                <p>This link will expire in 15 minutes.</p>
                <p>If you did not request this, please ignore this email.</p>
            ",
                IsBodyHtml = true
            };
            message.To.Add(toEmail);

            using var client = new SmtpClient(_emailSettings.SmtpServer, _emailSettings.Port)
            {
                Credentials = new NetworkCredential(_emailSettings.SenderEmail, _emailSettings.SenderPassword),
                EnableSsl = true
            };

            await client.SendMailAsync(message);
        }
    }
}
