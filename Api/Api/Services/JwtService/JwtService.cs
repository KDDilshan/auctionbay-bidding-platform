using Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Services.JwtService
{
    public class JwtService:IJwtService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public JwtService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public string GenerateToken(AppUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII
                .GetBytes(_configuration.GetSection("JWTSetting:securityKey").Value!); // Use ':' for nested sections

            var roles = _userManager.GetRolesAsync(user).Result;

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),
        new Claim(JwtRegisteredClaimNames.Name, user.FirstName ?? ""),
        new Claim(JwtRegisteredClaimNames.NameId, user.Id ?? ""),
        new Claim(JwtRegisteredClaimNames.Aud, _configuration.GetSection("JWTSetting:validAudience").Value!),
        new Claim(JwtRegisteredClaimNames.Iss, _configuration.GetSection("JWTSetting:validIssuer").Value!)
    };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
