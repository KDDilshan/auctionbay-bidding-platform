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
                .ForMember(dest => dest.NftDescription, opt => opt.MapFrom(src => src.Nft.description))
                .ForMember(dest => dest.FinalPrice, opt => opt.MapFrom(src => src.Bids.OrderByDescending(b => b.BidPrice).FirstOrDefault().BidPrice))
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Bids.OrderByDescending(b => b.BidPrice).FirstOrDefault().UserId))
                .ForMember(dest => dest.BuyerId, opt => opt.MapFrom(src => src.Bids.OrderByDescending(b => b.BidPrice).FirstOrDefault().UserId));

        }
    }
}
