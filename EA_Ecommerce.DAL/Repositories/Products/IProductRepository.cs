using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Products
{
    public interface IProductRepository : IGenericRepository<Product> {
        Task DecreaseQunatityAsync(List<(int productId, int quantity)> items);
        Task<List<Product>> GetAllProductsWithImagesAsync();
        Task<Product?> GetProductAsync(int id);
        Task<bool> DeleteProductAsync(int id);
    }
}
