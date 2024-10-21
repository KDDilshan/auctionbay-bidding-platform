using Api.Dtos;
using System;

namespace Api.Models.Email
{
    public class AuctionWinnerClaimedEmail : EmailBuilder
    {
        private Email email;
        private string auctionName;
        private string winnerName;
        private string winnerEmail;
        private string nftName;
        private string nftDescription;
        private string nftLink;

        public AuctionWinnerClaimedEmail(string auctionName, string winnerName, string winnerEmail, string nftName, string nftDescription)
        {
            this.auctionName = auctionName;
            this.winnerName = winnerName;
            this.winnerEmail = winnerEmail;
            this.nftName = nftName;
            this.nftDescription = nftDescription;
            this.nftLink = "http://localhost:3000/account/inventory";
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
                        <h2 style='color: #333333; text-align: center;'>Congratulations {winnerName}!</h2>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Dear {winnerName},
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            We are thrilled to inform you that you have successfully claimed your NFT from the auction <strong>{auctionName}</strong>. Thank you for your payment, and we are pleased to confirm that the NFT is now yours!
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Here are the details of the NFT you have won:
                        </p>
                        <ul style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            <li><strong>Name:</strong> {nftName}</li>
                            <li><strong>Description:</strong> {nftDescription}</li>
                        </ul>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            You can view and manage your NFT by following the link below:
                        </p>
                        <div style='text-align: center; margin: 20px 0;'>
                            <a href='{nftLink}' style='background-color: #007BFF; color: white; padding: 10px 20px; text-decoration: none; font-size: 16px; border-radius: 5px;'>View Your NFT</a>
                        </div>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Thank you for participating in the auction, and we hope you enjoy your new NFT!
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
            email.subject = $"Congratulations! You Have Claimed Your NFT from {auctionName}";
        }

        public void BuildTo()
        {
            email.to = winnerEmail;
        }
    }
}
