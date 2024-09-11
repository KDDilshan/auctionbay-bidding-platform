using Api.Dtos;

namespace Api.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(EmailDto request);
        public void Disconnect();
    }
}
