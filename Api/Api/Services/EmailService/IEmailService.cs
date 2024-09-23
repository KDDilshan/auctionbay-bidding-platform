using Api.Models.Email;

namespace Api.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(Email request);
        void Send(EmailBuilder emailBuilder);
        public void Disconnect();
    }
}
