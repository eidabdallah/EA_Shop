using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.utils.SeedData
{
    public class SeedData : ISeedData
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public SeedData(ApplicationDbContext context , RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager) {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task DataSeedingAsync()
        {
            if ((await _context.Database.GetPendingMigrationsAsync()).Any())
            {
                await _context.Database.MigrateAsync();
            }
            // add : 
            if (! await _context.Categories.AnyAsync()) { 
                await _context.Categories.AddRangeAsync(
                    new Category { Name = "Clothes"},
                    new Category { Name = "Mobile" }
                    );
            }
            if (! await _context.Brands.AnyAsync()){
                await _context.Brands.AddRangeAsync(
                    new Brand { Name = "Samsung" },
                    new Brand { Name = "Apple" },
                    new Brand { Name = "Nike" }
                    );
            }
            _context.SaveChangesAsync();

        }

        public async Task IdentityDataSeedingAsync()
        {
            // add roles 
            if(!await _roleManager.Roles.AnyAsync())
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            // add user : 
            if (!await _userManager.Users.AnyAsync())
            {
                var user1 = new ApplicationUser()
                {
                    FullName = "Eid Abdalla",
                    UserName = "eidAb",
                    PhoneNumber = "0505432540",
                    Email = "eidabdallah971@gmail.com"
                };
                var user2 = new ApplicationUser()
                {
                    FullName = "sami Abdalla",
                    UserName = "samiAb",
                    PhoneNumber = "0505432541",
                    Email = "samiabdallah971@gmail.com"
                };
                var user3 = new ApplicationUser()
                {
                    FullName = "Noor Abdalla",
                    UserName = "noorAb",
                    PhoneNumber = "0505432542",
                    Email = "noorabdallah971@gmail.com"
                };
                await _userManager.CreateAsync(user1 , "Pass@1212");
                await _userManager.CreateAsync(user2, "Pass@1213");
                await _userManager.CreateAsync(user3, "Pass@1214");

                await _userManager.AddToRoleAsync(user1, "SuperAdmin");
                await _userManager.AddToRoleAsync(user2, "Admin");
                await _userManager.AddToRoleAsync(user3, "User");

                await _context.SaveChangesAsync();
            }

        }
    }
}
