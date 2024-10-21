using Api.Dtos;
using Api.Entities;

namespace Api.Models.Email
{
    public class AuctionCreatedEmail : EmailBuilder
    {
        private Email email;
        private string auctionName;
        private DateTime startDateTime;
        private DateTime endDateTime;
        private string auctionOwnerName;
        private string auctionOwnerEmail;
        private string auctionLink;

        public AuctionCreatedEmail(Auction auction,AppUser user)
        {
            this.auctionName = auction.Title;
            this.startDateTime = auction.StartDate;
            this.endDateTime = auction.EndDate;
            this.auctionOwnerName = user.FirstName;
            this.auctionOwnerEmail = user.Email;
            this.auctionLink = "http://localhost:3000/auction/"+auction.Id;
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
                        <h2 style='color: #333333; text-align: center;'>Your Auction for {auctionName} is Live!</h2>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Dear {auctionOwnerName},
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            We are excited to inform you that your auction <strong>{auctionName}</strong> has been successfully created and is now live! Your auction is scheduled to start on <strong>{startDateTime:MMMM dd, yyyy hh:mm tt}</strong> and will close on <strong>{endDateTime:MMMM dd, yyyy hh:mm tt}</strong>.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            You can view and manage your auction by following the link below:
                        </p>
                        <div style='text-align: center; margin: 20px 0;'>
                            <a href='{auctionLink}' style='background-color: #007BFF; color: white; padding: 10px 20px; text-decoration: none; font-size: 16px; border-radius: 5px;'>View Your Auction</a>
                        </div>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            We wish you the best of luck with your auction! If you need any assistance or have questions, feel free to reach out to our support team.
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
            email.subject = $"Auction Created: {auctionName} is Now Live!";
        }

        public void BuildTo()
        {
            email.to = auctionOwnerEmail;
        }
    }
}
