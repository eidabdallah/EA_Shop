using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.Data.Migrations;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Categories;
using EA_Ecommerce.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Products
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task DecreaseQunatityAsync(List<(int productId, int quantity)> items)
        {
            var productIds = items.Select(i => i.productId).ToList();
            var products = await _context.Products.Where(p => productIds.Contains(p.Id)).ToListAsync();


            foreach (var product in products)
            { 
               var item = items.First(i => i.productId == product.Id);
                if (product.Quantity < item.quantity)
                {
                     throw new Exception($"Insufficient stock for product ID {product.Id}.");
                }
                product.Quantity -= item.quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsWithImagesAsync()
        {
            return await _context.Products
                .Include(p => p.ProductImages).Include(p=> p.Reviews).ThenInclude(r=> r.User)
                .ToListAsync();
        }
        public async Task<Product?> GetProductAsync(int id)
        {
            return await _context.Products.AsNoTracking().Include(p => p.ProductImages).Include(p => p.Reviews).ThenInclude(r => r.User).FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
                return false;
            if (product.ProductImages.Any())
                _context.productImages.RemoveRange(product.ProductImages);
            if (product.Reviews.Any())
                _context.Reviews.RemoveRange(product.Reviews);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }



    }
}
