using Api.Data;
using Api.Entities;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Api.Services.UserService
{
    public class UserService: IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;
        public UserService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
        public async Task<AppUser> getCurrentUser() 
        {
            String id = GetCurrentUserId();
            return await _userManager.FindByIdAsync(id);
        }
    }
}
