using Azure;
using EA_Ecommerce.DAL.DTO.Requests.Category;
using EA_Ecommerce.DAL.DTO.Requests.Product;
using EA_Ecommerce.DAL.DTO.Responses.Category;
using EA_Ecommerce.DAL.DTO.Responses.Product;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Products
{
    public interface IProductService : IGenericService<ProductRequestDTO, ProductResponseDTO, Product> {
       Task<int> CreateWithImage(ProductRequestDTO request);
        Task<List<ProductResponseDTO>> GetAllProductAsync(bool onlyActive = false);
    }
}
