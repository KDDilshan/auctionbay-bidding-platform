using Api.Dtos;
using System;

namespace Api.Models.Email
{
    public class AccountBlockedEmail : EmailBuilder
    {
        private Email email;
        private string userName;
        private string userEmail;
        private string supportEmail;

        public AccountBlockedEmail(string userName, string userEmail)
        {
            this.userName = userName;
            this.userEmail = userEmail;
            this.supportEmail = "servicenftfy@gmail.com";
            this.email = new Email();
        }

        public Email Build()
        {
            return email;
        }

        public void BuildBody()
        {
            email.body = $@"
                <div style='font-family: Arial, sans-serif; background-color: #f4f4f4; padding: 20px;'>
                    <div style='max-width: 600px; margin: 0 auto; background-color: #ffffff; padding: 20px; border-radius: 8px; box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);'>
                        <h2 style='color: #ff0000; text-align: center;'>Account Blocked</h2>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Dear {userName},
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            We regret to inform you that your account on NFTFY has been temporarily blocked. This action may have been taken due to policy violations or suspicious activity associated with your account.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            If you believe this was an error or wish to request an unblock, please reach out to our customer support team. We will be happy to assist you and discuss your options moving forward.
                        </p>
                        <p style='text-align: center; margin: 20px 0;'>
                            <a href='mailto:{supportEmail}' style='background-color: #007BFF; color: white; padding: 10px 20px; text-decoration: none; font-size: 16px; border-radius: 5px;'>Contact Customer Support</a>
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            We apologize for any inconvenience this may have caused and appreciate your understanding and cooperation.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Best Regards,<br>
                            <span style='color: #333333; font-weight: bold;'>The NFTFY Team</span>
                        </p>
                    </div>
                    <div style='max-width: 600px; margin: 0 auto; text-align: center; padding: 10px; font-size: 12px; color: #999999;'>
                        <p>
                            Please do not reply to this email. This is an automated message from NFTFY.<br>
                            For assistance, please contact our support team at <a href='mailto:{supportEmail}' style='color: #007BFF;'>{supportEmail}</a>.
                        </p>
                        <p>
                            © {DateTime.UtcNow.Year} NFTFY, All rights reserved.
                        </p>
                    </div>
                </div>";
        }

        public void BuildSubject()
        {
            email.subject = "Your NFTFY Account Has Been Blocked";
        }

        public void BuildTo()
        {
            email.to = userEmail;
        }
    }
}
