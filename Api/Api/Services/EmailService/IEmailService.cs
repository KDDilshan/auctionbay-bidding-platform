using Api.Dtos;
using Api.Models;

namespace Api.Services.EmailService
{
    public interface IEmailService
    {
        void SendEmail(Email request);
        public void Disconnect();
    }
}
