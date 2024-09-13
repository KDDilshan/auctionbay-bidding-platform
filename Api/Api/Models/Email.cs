using Api.Dtos;
using Api.Entities;

namespace Api.Models
{
    public class Email
    {
        public string to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

        Email(EmailBuilder builder)
        {
            this.to = builder.to;
            this.subject = builder.subject;
            this.body = builder.body;
        }

        public class EmailBuilder
        {
            public string to { get; set; }
            public string subject { get; set; }
            public string body { get; set; }
            private UserDto user { get; set; }

            public EmailBuilder To(UserDto user)
            {
                this.to = user.Email;
                this.user = user;
                return this;
            }

            public EmailBuilder ToEmail(string email)
            {
                this.to = email;
                return this;
            }

            public EmailBuilder Subject(string subject)
            {
                this.subject = subject;
                return this;
            }

            public EmailBuilder Body(string body)
            {
                this.body = body;
                return this;
            }

            public Email Build()
            {
                return new Email(this);
            }

            public EmailBuilder Register()
            {
                this.subject = "Welcome to Nftfy – Registration Successful!";
                this.body = $"<h3>Welcome {user.FirstName} {user.LastName},</h3>" +
                            "<p>Thank you for registering with us. We are excited to have you on board.</p>" +
                            "<p>Best Regards,<br>" +
                            "NFTFY Team</p>";
                return this;
            }
        }
    }
}
