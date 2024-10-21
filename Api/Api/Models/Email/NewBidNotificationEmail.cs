using Api.Dtos;

namespace Api.Models.Email
{
    public class NewBidNotificationEmail : EmailBuilder
    {
        private Email email;
        private string auctionName;
        private decimal bidAmount;
        private string bidderName;
        private string auctionOwnerName;
        private string auctionOwnerEmail;

        public NewBidNotificationEmail(string auctionName, decimal bidAmount, string bidderName, string auctionOwnerName, string auctionOwnerEmail)
        {
            this.auctionName = auctionName;
            this.bidAmount = bidAmount/100;
            this.bidderName = bidderName;
            this.auctionOwnerName = auctionOwnerName;
            this.auctionOwnerEmail = auctionOwnerEmail;
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
                        <h2 style='color: #333333; text-align: center;'>New Bid Received for {auctionName}!</h2>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Dear {auctionOwnerName},
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            We are excited to inform you that a new bid of <strong>${bidAmount}</strong> has been placed on your auction <strong>{auctionName}</strong> by <strong>{bidderName}</strong>.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Keep an eye on your auction as more bids might come in soon! You can view the latest status of your auction by logging into your account.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Best of luck with your auction!
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
            email.subject = $"New Bid Alert: ${bidAmount} on {auctionName}";
        }

        public void BuildTo()
        {
            email.to = auctionOwnerEmail;
        }
    }
}
