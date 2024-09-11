using Api.Dtos;
using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace Api.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly SmtpClient _smtpClient;
        public EmailService(IConfiguration config, SmtpClient smtpClient)
        {
            _config = config;
            _smtpClient = smtpClient;
            Connect();
        }
        public void SendEmail(EmailDto request)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("EmailUsername").Value));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = request.Body
            };
            Connect();
            _smtpClient.Send(email);
        }

        private void Connect()
        {
            if (!_smtpClient.IsConnected)
            {
                _smtpClient.Connect(_config.GetSection("EmailHost").Value, 587, SecureSocketOptions.StartTls);
                _smtpClient.Authenticate(_config.GetSection("EmailUsername").Value, _config.GetSection("EmailPassword").Value);
            }
        }
        public void Disconnect()
        {
            if (_smtpClient.IsConnected)
            {
                _smtpClient.Disconnect(true);
            }
        }
    }
}
