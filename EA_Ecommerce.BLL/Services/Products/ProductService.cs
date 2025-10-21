using EA_Ecommerce.BLL.Services.Categories;
using EA_Ecommerce.BLL.Services.Files;
using EA_Ecommerce.DAL.DTO.Requests.Category;
using EA_Ecommerce.DAL.DTO.Requests.Product;
using EA_Ecommerce.DAL.DTO.Responses.Category;
using EA_Ecommerce.DAL.DTO.Responses.Product;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Brands;
using EA_Ecommerce.DAL.Repositories.Categories;
using EA_Ecommerce.DAL.Repositories.Generic;
using EA_Ecommerce.DAL.Repositories.Products;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Products
{
    public class ProductService : GenericService<ProductRequestDTO, ProductResponseDTO, Product>, IProductService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;
        public ProductService(IProductRepository productRepository , IFileService fileService 
            , ICategoryRepository categoryRepository , IBrandRepository brandRepository) : base(productRepository , fileService) {
           _categoryRepository = categoryRepository;
           _brandRepository = brandRepository;
        }

        public async Task<int> CreateWithImage(ProductRequestDTO request)
        {
            if (await _categoryRepository.GetByIdAsync(request.CategoryId) is null)
                throw new Exception("Category Not Found");

            if (request.BrandId is int brandId &&
                await _brandRepository.GetByIdAsync(brandId) is null)
                throw new Exception("Brand Not Found");
            return await base.CreateAsync(request, true, "product");
        }
    }
}
