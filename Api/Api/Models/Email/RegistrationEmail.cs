using Api.Dtos;

namespace Api.Models.Email
{
    public class RegistrationEmail : EmailBuilder
    {
        private Email email;
        private UserDto user;

        public RegistrationEmail(UserDto user)
        {
            this.user = user;
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
                        <h2 style='color: #333333; text-align: center;'>Welcome to NFTFY, {user.FirstName} {user.LastName}!</h2>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Thank you for joining NFTFY – the premier NFT auction house. We're thrilled to have you in our community!
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            As a registered member, you can now explore, bid on, and create unique NFTs. Start discovering exclusive auctions, and showcase your own digital creations to the world.
                        </p>
                        <p style='color: #555555; font-size: 16px; line-height: 1.6;'>
                            Stay tuned for upcoming auctions and updates from the NFTFY community. Should you need any assistance, our support team is here to help.
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
            email.subject = "Welcome to NFTFY – Your NFT Auction Journey Begins!";
        }

        public void BuildTo()
        {
            email.to = user.Email;
        }
    }
}
