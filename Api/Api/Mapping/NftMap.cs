using Api.Dtos;
using Api.Entities;

namespace Api.Mapping
{
    public static class NftMap
    {
        public static Nft ToEntity(this NftDto dto, string userId, string path)
        {
            return new Nft
            {
                UserId = userId,
                Title = dto.Title,
                Description = dto.Description,
                Image = path
            };
        }
    }
}
