using EA_Ecommerce.BLL.Services.Files;
using EA_Ecommerce.DAL.DTO.Requests.Category;
using EA_Ecommerce.DAL.DTO.Requests.Product;
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
        private readonly ICategoryRepository _categoryRepository;
        private readonly IFileService _fileService;

        public CategoryService(ICategoryRepository categoryRepository , IFileService fileService):base(categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _fileService = fileService;
        }
        public async Task<int> CreateWithImage(CategoryRequestDTO request)
        {
            var entity = request.Adapt<Category>();
            entity.CreatedAt = DateTime.UtcNow;


            if (request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                entity.MainImage = imagePath;
            }
            return await _categoryRepository.CreateAsync(entity);
        }
    }
}

