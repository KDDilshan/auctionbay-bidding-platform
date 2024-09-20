using Api.Dtos;
using Api.Entities;
using AutoMapper;

namespace Api.Mapping
{
    public class CheakoutMapper : Profile
    {
        public CheakoutMapper() 
        {
            CreateMap<Auction, CheakoutDto>()
                .ForMember(dest => dest.NftTitle, opt => opt.MapFrom(src => src.Nft.Title))
                .ForMember(dest => dest.NftDescription, opt => opt.MapFrom(src => src.Nft.Description))
                .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src =>
                    src.Bids.OrderByDescending(b => b.BidPrice).FirstOrDefault() != null
                        ? src.Bids.OrderByDescending(b => b.BidPrice).First().BidPrice
                        : 0)) // Default to 0 if there are no bids
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src =>
                    src.Bids.OrderByDescending(b => b.BidPrice).FirstOrDefault() != null
                        ? src.Bids.OrderByDescending(b => b.BidPrice).First().UserId
                        : null)) // Default to null if there are no bids
                .ForMember(dest => dest.BuyerUsername, opt => opt.MapFrom(src =>
                    src.Bids.OrderByDescending(b => b.BidPrice).FirstOrDefault() != null
                        ? src.Bids.OrderByDescending(b => b.BidPrice).First().AppUsers.FirstName 
                        : null));


            
        }   
    }
}
