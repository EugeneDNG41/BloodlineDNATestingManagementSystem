using Data.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit.Text;
using MimeKit;
using Org.BouncyCastle.Asn1.Pkcs;

namespace Web
{
    public class EmailSender : IEmailSender<User>
    {
        private readonly MailConfiguration _configuration;
        private readonly ISmtpClient _smtpClient;
        public EmailSender(IOptions<MailConfiguration> options,
            ISmtpClient smtpClient)
        {
            _configuration = options.Value;
            _smtpClient = smtpClient;
        }
        private async Task<bool> SendMailAsync(MailData mailData)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_configuration.SenderName, _configuration.FromEmail));
            message.To.Add(new MailboxAddress(mailData.ReceiverName, mailData.ToEmail));
            message.Subject = mailData.Subject;
            message.Body = new TextPart(TextFormat.Html) { Text = mailData.Body };

            try
            {
                if (!_smtpClient.IsConnected)
                    await _smtpClient.ConnectAsync(_configuration.Server, _configuration.Port, SecureSocketOptions.StartTls);

                if (!_smtpClient.IsAuthenticated)
                    await _smtpClient.AuthenticateAsync(_configuration.FromEmail, _configuration.Password);

                await _smtpClient.SendAsync(message);
                await _smtpClient.DisconnectAsync(true);
                return true;
            }
            catch
            {
                // Consider logging the exception
                return false;
            }
        }
        public async Task SendConfirmationLinkAsync(User user, string email, string confirmationLink)
        {
            var mail = new MailData
            {
                ReceiverName = user.UserName ?? email,
                ToEmail = email,
                Subject = "Confirm your account",
                Body = $"<p>Hello {user.UserName ?? "User"},</p>" +
                       $"<p>Please confirm your account by clicking the link below:</p>" +
                       $"<p><a href='{confirmationLink}'>Confirm Account</a></p>"
            };
            await SendMailAsync(mail);
        }

        public async Task SendPasswordResetLinkAsync(User user, string email, string resetLink)
        {
            var mail = new MailData
            {
                ReceiverName = user.UserName ?? email,
                ToEmail = email,
                Subject = "Reset your password",
                Body = $"<p>Hello {user.UserName ?? "User"},</p>" +
                       $"<p>You can reset your password by clicking the link below:</p>" +
                       $"<p><a href='{resetLink}'>Reset Password</a></p>"
            };
            await SendMailAsync(mail);
        }

        public async Task SendPasswordResetCodeAsync(User user, string email, string resetCode)
        {
            var mail = new MailData
            {
                ReceiverName = user.UserName ?? email,
                ToEmail = email,
                Subject = "Your password reset code",
                Body = $"<p>Hello {user.UserName ?? "User"},</p>" +
                       $"<p>Your password reset code is:</p>" +
                       $"<h2>{resetCode}</h2>"
            };
            await SendMailAsync(mail);
        }
    }
    public class MailConfiguration
    {
        public string Server { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string FromEmail { get; set; }
        public string Password { get; set; }
    }
    public class MailData
    {
        public required string ReceiverName { get; set; }
        public required string ToEmail { get; set; }
        public required string Subject { get; set; }
        public required string Body { get; set; }


    }
}
