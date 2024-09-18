using Api.Dtos;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NftCheckoutController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private static string s_wasmClientURL = string.Empty;

        public NftCheckoutController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<ActionResult> CheakoutNftOrder([FromBody] CheakoutDto cheakoutDto, [FromServices] IServiceProvider sp)
        {
            var referer = Request.Headers.Referer;
            s_wasmClientURL = referer[0];

            var server = sp.GetRequiredService<IServer>();

            var serverAddressesFeature = server.Features.Get<IServerAddressesFeature>();

            string? thisApiUrl = null;

            if (serverAddressesFeature is not null)
            {
                thisApiUrl = serverAddressesFeature.Addresses.FirstOrDefault();
            }


            if (thisApiUrl is not null)
            {
                var sessionId= await CheakOut(cheakoutDto, thisApiUrl);
                var pubKey = _configuration["Stripe:PubKey"];

                var checkoutOrderResponse = new CheakoutOrderResponse()
                {
                    SessionId = sessionId,
                    PubKey = pubKey
                };

                return Ok(checkoutOrderResponse);
            }
            else
            {
                return StatusCode(500);
            }

        }

        [NonAction]
        public async Task<string> CheakOut(CheakoutDto cheakoutDto,string thisApiUrl)
        {
            var options=new SessionCreateOptions
            {
                SuccessUrl = $"{thisApiUrl}/checkout/success?sessionId=" + "{CHECKOUT_SESSION_ID}", // Customer paid.
                CancelUrl = s_wasmClientURL + "failed",  // Checkout cancelled.
                PaymentMethodTypes = new List<string> // Only card available in test mode?
                {
                    "card"
                },

                LineItems = new List<SessionLineItemOptions>
                {
                    new()
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = cheakoutDto.FinalPrice, // Price is in USD cents.
                            Currency = "USD",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = cheakoutDto.NftTitle,
                                Description = cheakoutDto.NftDescription,
                                Images = new List<string> { cheakoutDto.NftImageUrl }
                            },
                        },
                        Quantity = 1,
                    },
                },
                Mode = "payment" // One-time payment. Stripe supports recurring 'subscription' payments.
            };

            var service = new SessionService();
            var session = await service.CreateAsync(options);

            return session.Id;

        }
    }
}

