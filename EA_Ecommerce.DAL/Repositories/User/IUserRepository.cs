using EA_Ecommerce.DAL.Models;

namespace EA_Ecommerce.DAL.Repositories.User
{
    public interface IUserRepository
    {
        Task<List<ApplicationUser>> GetAllAsync();
        Task<ApplicationUser> GetByIdAsync(string UserId);

        Task<bool> BlockUserAsync(string userId , int days);
        Task<bool> UnBlockUserAsync(string userId);
        Task<bool> IsBlockedAsync(string userId);

        Task<bool> ChangeUserRoleAsync(string userId, string roleName);

    }
}
