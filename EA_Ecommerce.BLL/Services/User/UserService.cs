using EA_Ecommerce.DAL.DTO.Responses.User;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.User;
using Mapster;
using Microsoft.AspNetCore.Identity;

namespace EA_Ecommerce.BLL.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(IUserRepository userRepository , UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _userManager = userManager;
        }
        public async Task<List<UserInfoResponseDTO>> GetAllAsync()
        {
           var users = await _userRepository.GetAllAsync();
           var userList = new List<UserInfoResponseDTO>();
            foreach (var user in users)
            {
                var role = await _userManager.GetRolesAsync(user);
                userList.Add(new UserInfoResponseDTO
                {
                    Email = user.Email!,
                    EmailConfirmed = user.EmailConfirmed,
                    FullName = user.FullName,
                    Id = user.Id,
                    PhoneNumber = user.PhoneNumber!,
                    Role = role.FirstOrDefault()!,
                    UserName = user.UserName!

                });
            }
            return userList;
        }

        public async Task<UserInfoResponseDTO?> GetByIdAsync(string UserId)
        {
            var user = await _userRepository.GetByIdAsync(UserId);
            if (user is null)
            {
                throw new Exception("user not Found");
            }
            var role = await _userManager.GetRolesAsync(user);
            var userInfo = new UserInfoResponseDTO()
            {
                Email = user.Email!,
                EmailConfirmed = user.EmailConfirmed,
                FullName = user.FullName,
                Id = user.Id,
                PhoneNumber = user.PhoneNumber!,
                Role = role.FirstOrDefault()!,
                UserName = user.UserName!
            };
            return userInfo;

        }

        public async Task<bool> IsBlockedAsync(string userId)
        {
            return await _userRepository.IsBlockedAsync(userId);
        }
        public async Task<bool> BlockUserAsync(string userId, int days)
        {
            return await _userRepository.BlockUserAsync(userId, days);
        }

        public async Task<bool> UnBlockUserAsync(string userId)
        {
            return await _userRepository.UnBlockUserAsync(userId);
        }

        public async Task<bool> ChangeUserRoleAsync(string userId, string roleName)
        {
            return await _userRepository.ChangeUserRoleAsync(userId, roleName);
        }
    }
}
