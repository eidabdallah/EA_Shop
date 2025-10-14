using EA_Ecommerce.DAL.DTO.Requests.Category;
using EA_Ecommerce.DAL.DTO.Responses.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Categories
{
    public interface ICategoryService
    {
        int CreateCategory(CategoryRequestDTO request);
        IEnumerable<CategoryResponseDTO> GetAllCategories();
        CategoryResponseDTO? GetCategoryById(int id);
        int UpdateCategory(int id, CategoryRequestDTO request);
        int DeleteCategory(int id);
        bool ToggleStatus(int id);
    }
}
