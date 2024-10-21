using Api.Data;
using Api.Dtos;
using Api.Entities;
using Api.Services.BidService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace Api.Services.PaymentService
{
    public class StripePaymentService
    {
        public StripePaymentService(IOptions<StripeSettings> stripeSettings)
        {
            StripeConfiguration.ApiKey = stripeSettings.Value.SecretKey;
        }

        public Session CreateCheckoutSession(Auction auction,Bid highestBid)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = highestBid.BidPrice,
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = auction.Nft.Title,
                                Description = auction.Nft.Description
                            },
                        },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = "https://localhost:7218/api/Payment/confirm?sessionId={CHECKOUT_SESSION_ID}",
                CancelUrl = "https://localhost:7218/api/Payment/confirm?sessionId={CHECKOUT_SESSION_ID}",
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }

        public async Task<Session> CheckSession(string sessionId)
        {
            var service = new SessionService();
            Session session = await service.GetAsync(sessionId);
            return session;
        }

    }
}
