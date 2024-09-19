using Api.Entities;

namespace Api.Services.JwtService
{
    public interface IJwtService
    {
        public string GenerateToken(AppUser user);
    }
}
