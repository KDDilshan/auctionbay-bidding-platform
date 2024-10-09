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

        public static SellerRequestResponse ToDto(this SellerRequest req)
        {
            return new SellerRequestResponse
            {
                Id = req.Id,
                Name = req.User.FirstName +" "+ req.User.LastName,
                Email = req.User.Email,
                Created = req.RequestDate,
                Address = req.Address,
                Dob = req.DateOfBirth,
                Status = req.Status,
            };
        }
    }
}
