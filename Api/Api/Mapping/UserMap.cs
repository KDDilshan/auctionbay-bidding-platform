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
                UserName = registerDto.Email
            };
        }
    }
}
