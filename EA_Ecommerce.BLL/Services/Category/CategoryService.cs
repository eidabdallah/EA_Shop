using EA_Ecommerce.DAL.DTO.Requests.Category;
using EA_Ecommerce.DAL.DTO.Responses.Category;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Categories;
using EA_Ecommerce.DAL.Repositories.Generic;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Categories
{
    public class CategoryService : GenericService<CategoryRequestDTO, CategoryResponseDTO, Category>, ICategoryService
    {
        public CategoryService(ICategoryRepository categoryRepository):base(categoryRepository){}        
    }
}

