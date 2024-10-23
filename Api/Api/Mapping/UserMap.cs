using Api.Dtos;
using Api.Entities;

namespace Api.Mapping
{
    public static class UserMap
    {
        public static AppUser ToEntity(this RegisterDto registerDto)
        {
            return new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName = registerDto.Email,
                Status = "Active",
            };
        }

        public static UserDto ToDto(this AppUser appUser)
        {
            return new UserDto
            {
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Email = appUser.Email,
                Status = appUser.Status,
            };
        }
        public static UserDto ToDto(this AppUser appUser,string role)
        {
            return new UserDto
            {
                FirstName = appUser.FirstName,
                LastName = appUser.LastName,
                Email = appUser.Email,
                Status = appUser.Status,
                Role = role
            };
        }
    }
}
