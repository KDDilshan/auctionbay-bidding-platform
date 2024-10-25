using Api.Dtos;
using System;

namespace Api.Models.Email
{
    public class SellerRequestDeclinedEmail : EmailBuilder
    {
        private Email email;
        private string userName;
        private string userEmail;
        private string retryLink;

        public SellerRequestDeclinedEmail(string userName, string userEmail)
        {
            this.userName = userName;
            this.userEmail = userEmail;
            this.retryLink = "http://localhost:3000/account/seller";
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
                        <h2 style='color: #333333; text-align: center;'>Request Declined</h2>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Dear {userName},
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            We regret to inform you that your request to become an NFT seller on our platform has been declined at this time.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            However, this is not the end of your journey! We encourage you to review our seller guidelines and feel free to reapply once you have made any necessary adjustments.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            You can find more details and reapply using the link below:
                        </p>
                        <div style='text-align: center; margin: 20px 0;'>
                            <a href='{retryLink}' style='background-color: #007BFF; color: white; padding: 10px 20px; text-decoration: none; font-size: 16px; border-radius: 5px;'>Apply Again</a>
                        </div>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            We appreciate your interest in becoming part of our NFT seller community and look forward to seeing your next application!
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
            email.subject = $"Your NFT Seller Request Has Been Declined";
        }

        public void BuildTo()
        {
            email.to = userEmail;
        }
    }
}
