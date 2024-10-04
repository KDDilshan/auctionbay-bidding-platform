using Api.Dtos;
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

        public Session CreateCheckoutSession(string successUrl, string cancelUrl,CheakoutDto cheakoutDto)
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
                            UnitAmount = cheakoutDto.FinalPrice * 100, // For example, $20.00 (this value is in cents)
                            Currency = "usd",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = cheakoutDto.NftTitle,
                                Description = cheakoutDto.NftDescription

                            },
                        },
                    Quantity = 1,
                },
            },
                Mode = "payment",
                SuccessUrl = successUrl,
                CancelUrl = cancelUrl,
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return session;
        }
    
    }
}
