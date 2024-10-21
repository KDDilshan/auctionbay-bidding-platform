using Api.Dtos;
using Api.Entities;

namespace Api.Mapping
{
    public static class PaymentMap
    {
        public static TransactionResponse ToDto(this PaymentRecord paymentRecord,long highest) 
        {
            return new TransactionResponse
            {
                Id = paymentRecord.StripeSessionId,
                Auction = paymentRecord.Auction.Title,
                Nft = paymentRecord.Auction.Nft.Title,
                Date = paymentRecord.Created,
                Amount = highest,
            };

        }
    }
}
