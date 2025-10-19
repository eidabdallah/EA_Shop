using EA_Ecommerce.DAL.DTO.Requests.Category;
using EA_Ecommerce.DAL.DTO.Requests.Product;
using EA_Ecommerce.DAL.DTO.Responses.Category;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Categories
{
    public interface ICategoryService : IGenericService<CategoryRequestDTO,CategoryResponseDTO,Category> {
        Task<int> CreateWithImage(CategoryRequestDTO request);
    }
}
