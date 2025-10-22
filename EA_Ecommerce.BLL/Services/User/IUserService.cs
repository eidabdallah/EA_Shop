using EA_Ecommerce.DAL.DTO.Responses.User;
using EA_Ecommerce.DAL.Models;

namespace EA_Ecommerce.BLL.Services.User
{
    public interface IUserService
    {
        Task<List<UserInfoResponseDTO>> GetAllAsync();
        Task<UserInfoResponseDTO?> GetByIdAsync(string UserId);
        Task<bool> BlockUserAsync(string userId, int days);
        Task<bool> UnBlockUserAsync(string userId);
        Task<bool> IsBlockedAsync(string userId);
        Task<bool> ChangeUserRoleAsync(string userId, string roleName);
    }
}
