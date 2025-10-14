using EA_Ecommerce.DAL.DTO.Requests.Category;
using EA_Ecommerce.DAL.DTO.Responses.Category;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Categories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public int CreateCategory(CategoryRequestDTO request)
        {
            var category = request.Adapt<Category>();
            return categoryRepository.Create(category);
        }

        public int DeleteCategory(int id)
        {
            var category = categoryRepository.GetById(id);
            if (category is null)
            {
                return 0;
            }
            return categoryRepository.Delete(category);
        }

        public IEnumerable<CategoryResponseDTO> GetAllCategories()
        {
            var categories = categoryRepository.GetAll();
            return categories.Adapt<IEnumerable<CategoryResponseDTO>>();
        }

        public CategoryResponseDTO? GetCategoryById(int id)
        {
           var category = categoryRepository.GetById(id);
           
            return category is null ? null : category.Adapt<CategoryResponseDTO>();
        }

        public int UpdateCategory(int id, CategoryRequestDTO request)
        {
            var category = categoryRepository.GetById(id);
            if (category is null)
            {
                return 0;
            }
            category.Name = request.Name;
            return categoryRepository.Update(category);
        }
        public bool ToggleStatus(int id)
        {
            var category = categoryRepository.GetById(id);
            if (category is null) return false;
            category.Status = category.Status == Status.Active ? Status.Inactive : Status.Active;
            categoryRepository.Update(category);
            return true;
        }
    }
}
