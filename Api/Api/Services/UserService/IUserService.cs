using Api.Entities;

namespace Api.Services.UserService
{
    public interface IUserService
    {
        public string GetCurrentUserId();
        public Task<AppUser> getCurrentUser();
        public string getRole(AppUser user);
    }
}
