using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;
using WebApi.Helpers;

namespace WebApi.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _email;

        public EmailService(IOptions<EmailSettings> email)
        {
            _email = email.Value;
        }

        public void Send(string to, string subject, string html, string from = null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from ?? _email.EmailFrom));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            using var smtp = new SmtpClient();
            smtp.Connect(_email.SmtpHost, _email.SmtpPort, SecureSocketOptions.StartTls);
            smtp.Authenticate(_email.SmtpUser, _email.SmtpPass);
            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}