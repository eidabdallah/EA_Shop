using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<List<ApplicationUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<ApplicationUser> GetByIdAsync(string UserId)
        {
            return await _userManager.FindByIdAsync(UserId);
        }
        public async Task<bool> BlockUserAsync(string userId, int days)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return false;
            user.LockoutEnd = DateTime.UtcNow.AddDays(days);
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded; // Return true if the operation was successful
        }

        public async Task<bool> UnBlockUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return false;
            user.LockoutEnd = null;
            var result = await _userManager.UpdateAsync(user);
            return result.Succeeded; // Return true if the operation was successful
        }
        public async Task<bool> IsBlockedAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) return false;
            return user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow;
        }

        public async Task<bool> ChangeUserRoleAsync(string userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user is null) return false;
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            var addResult = await _userManager.AddToRoleAsync(user, roleName);
            return removeResult.Succeeded && addResult.Succeeded;
        }
    }
}
