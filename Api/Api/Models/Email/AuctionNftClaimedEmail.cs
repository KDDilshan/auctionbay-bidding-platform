using Api.Dtos;
using System;

namespace Api.Models.Email
{
    public class AuctionNftClaimedEmail : EmailBuilder
    {
        private Email email;
        private string ownerName;
        private string ownerEmail;
        private string nftName;
        private string winnerName;
        private decimal winningBidAmount;
        private DateTime claimDate;

        public AuctionNftClaimedEmail(string ownerName, string ownerEmail, string nftName, string winnerName, decimal winningBidAmount, DateTime claimDate)
        {
            this.ownerName = ownerName;
            this.ownerEmail = ownerEmail;
            this.nftName = nftName;
            this.winnerName = winnerName;
            this.winningBidAmount = winningBidAmount/100;
            this.claimDate = claimDate;
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
                        <h2 style='color: #333333; text-align: center;'>Your NFT Has Been Successfully Claimed!</h2>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Dear {ownerName},
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            We’re pleased to inform you that the NFT titled <strong>{nftName}</strong> from your recent auction has been successfully claimed by the winning bidder.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            <strong>Claim Details:</strong><br>
                            <ul style='color: #555555; font-size: 16px; line-height: 1.6;'>
                                <li><strong>Winner:</strong> {winnerName}</li>
                                <li><strong>Winning Bid Amount:</strong> ${winningBidAmount}</li>
                                <li><strong>Claim Date:</strong> {claimDate.ToString("MMMM dd, yyyy")}</li>
                            </ul>
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Thank you for choosing NFTFY for your auction. We look forward to supporting your future auctions on our platform!
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Best Regards,<br>
                            <span style='color: #333333; font-weight: bold;'>The NFTFY Team</span>
                        </p>
                    </div>
                    <div style='max-width: 600px; margin: 0 auto; text-align: center; padding: 10px; font-size: 12px; color: #999999;'>
                        <p>
                            Please do not reply to this email. This is an automated message from NFTFY.<br>
                            For assistance, please contact our support team at <a href='mailto:support@nftfy.com' style='color: #007BFF;'>support@nftfy.com</a>.
                        </p>
                        <p>
                            © {DateTime.UtcNow.Year} NFTFY, All rights reserved.
                        </p>
                    </div>
                </div>";
        }

        public void BuildSubject()
        {
            email.subject = $"NFT '{nftName}' Successfully Claimed by Auction Winner";
        }

        public void BuildTo()
        {
            email.to = ownerEmail;
        }
    }
}
