using Api.Dtos;
using System;

namespace Api.Models.Email
{
    public class SellerRequestPlacedEmail : EmailBuilder
    {
        private Email email;
        private string userName;
        private string userEmail;

        public SellerRequestPlacedEmail(string userName, string userEmail)
        {
            this.userName = userName;
            this.userEmail = userEmail;
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
                        <h2 style='color: #333333; text-align: center;'>Your Request Has Been Received!</h2>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Dear {userName},
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Thank you for your interest in becoming an NFT seller on our platform! We have successfully received your request, and our team will review it shortly.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Please allow us up to 3-5 business days to process your request. We will notify you via email once a decision has been made.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            We appreciate your patience and look forward to the possibility of welcoming you as part of our NFT seller community.
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
            email.subject = $"Your NFT Seller Request Has Been Successfully Placed";
        }

        public void BuildTo()
        {
            email.to = userEmail;
        }
    }
}
