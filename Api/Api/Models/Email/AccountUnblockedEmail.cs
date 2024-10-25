using Api.Dtos;
using System;

namespace Api.Models.Email
{
    public class AccountUnblockedEmail : EmailBuilder
    {
        private Email email;
        private string userName;
        private string userEmail;
        private string loginLink;

        public AccountUnblockedEmail(string userName, string userEmail)
        {
            this.userName = userName;
            this.userEmail = userEmail;
            this.loginLink = "http://localhost:3000/login";
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
                        <h2 style='color: #28a745; text-align: center;'>Account Unblocked</h2>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Dear {userName},
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            We are pleased to inform you that your account on NFTFY has been successfully unblocked. You now have full access to all features and can continue using our platform as usual.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            To access your account, please log in using the link below:
                        </p>
                        <div style='text-align: center; margin: 20px 0;'>
                            <a href='{loginLink}' style='background-color: #007BFF; color: white; padding: 10px 20px; text-decoration: none; font-size: 16px; border-radius: 5px;'>Log in to Your Account</a>
                        </div>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            We appreciate your patience and look forward to serving you as part of our NFT community.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Best Regards,<br>
                            <span style='color: #333333; font-weight: bold;'>The NFTFY Team</span>
                        </p>
                    </div>
                    <div style='max-width: 600px; margin: 0 auto; text-align: center; padding: 10px; font-size: 12px; color: #999999;'>
                        <p>
                            Please do not reply to this email. This is an automated message from NFTFY.<br>
                            If you have any questions, please contact our support team at <a href='mailto:support@nftfy.com' style='color: #007BFF;'>support@nftfy.com</a>.
                        </p>
                        <p>
                            © {DateTime.UtcNow.Year} NFTFY, All rights reserved.
                        </p>
                    </div>
                </div>";
        }

        public void BuildSubject()
        {
            email.subject = "Your NFTFY Account Has Been Unblocked";
        }

        public void BuildTo()
        {
            email.to = userEmail;
        }
    }
}
