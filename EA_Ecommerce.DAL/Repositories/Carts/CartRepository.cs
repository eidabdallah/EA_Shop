using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Categories;
using EA_Ecommerce.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Carts
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
            return await _context.SaveChangesAsync();
        }
        public async Task<List<Cart>> GetUserCartAsync(string UserId)
        {
            return await _context.Carts.Include(c=> c.Product).Where(c => c.UserId == UserId).ToListAsync();
        }
        public async Task<bool> ClearCartAsync(string UserId)
        {
            var items = _context.Carts.Where(c => c.UserId == UserId).ToList();
            _context.Carts.RemoveRange(items);
            return true;
        }
        public async Task<bool> DeleteProductFromCartAsync(int ProductId, string UserId)
        {
            var item = await _context.Carts.FirstOrDefaultAsync(c => c.ProductId == ProductId && c.UserId == UserId);
            if (item != null)
            {
                _context.Carts.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateProductCountAsync(int ProductId, string Operation, string UserId)
        {
            var item = await _context.Carts.FirstOrDefaultAsync(c => c.ProductId == ProductId && c.UserId == UserId);
            if (item != null)
            {
                if (Operation == "increase")
                {
                    item.Count += 1;
                }
                else if (Operation == "decrease" && item.Count > 1)
                {
                    item.Count -= 1;
                }
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
    

