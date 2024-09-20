using Api.Dtos;
using Api.Entities;

namespace Api.Mapping
{
    public static class SellerReqMap
    {
        public static SellerRequest ToEntity(this SellerRequestDto dto, string userId, string path)
        {
            return new SellerRequest
            {
                UserId = userId,
                IdPhotoPath = path,
                Address = dto.Address,
                DateOfBirth = dto.DateOfBirth
            };
        }
    }
}
