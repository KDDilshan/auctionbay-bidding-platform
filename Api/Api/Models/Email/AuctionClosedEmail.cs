using Api.Dtos;

namespace Api.Models.Email
{
    public class AuctionClosedEmail : EmailBuilder
    {
        private Email email;
        private string auctionName;
        private DateTime closedDateTime;
        private decimal highestBidAmount;
        private string highestBidderName;
        private string auctionOwnerEmail;

        public AuctionClosedEmail(string auctionName, DateTime closedDateTime, decimal highestBidAmount, string highestBidderName, string auctionOwnerEmail)
        {
            this.auctionName = auctionName;
            this.closedDateTime = closedDateTime.ToLocalTime();
            this.highestBidAmount = highestBidAmount/100;
            this.highestBidderName = highestBidderName;
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
                        <h2 style='color: #333333; text-align: center;'>Auction Closed: {auctionName}</h2>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Your auction <strong>{auctionName}</strong> has successfully closed on <strong>{closedDateTime:MMMM dd, yyyy hh:mm tt}</strong>.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            The highest bid received is <strong>${highestBidAmount}</strong>, placed by <strong>{highestBidderName}</strong>.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Congratulations on a successful auction! If you have any questions or need assistance, feel free to contact our support team.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Best Regards,<br>
                            <span style='color: #333333; font-weight: bold;'>NFTFY Team</span>
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
            email.subject = $"Auction Closed: {auctionName} – Highest Bid Details";
        }

        public void BuildTo()
        {
            email.to = auctionOwnerEmail;
        }
    }
}
