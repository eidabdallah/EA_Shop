using EA_Ecommerce.BLL.Services.Categories;
using EA_Ecommerce.BLL.Services.Files;
using EA_Ecommerce.DAL.DTO.Requests.Category;
using EA_Ecommerce.DAL.DTO.Requests.Product;
using EA_Ecommerce.DAL.DTO.Responses.Category;
using EA_Ecommerce.DAL.DTO.Responses.Product;
using EA_Ecommerce.DAL.DTO.Responses.Review;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Brands;
using EA_Ecommerce.DAL.Repositories.Categories;
using EA_Ecommerce.DAL.Repositories.Generic;
using EA_Ecommerce.DAL.Repositories.Products;
using Mapster;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Products
{
    public class ProductService : GenericService<ProductRequestDTO, ProductResponseDTO, DAL.Models.Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IBrandRepository _brandRepository;

        public ProductService(IProductRepository productRepository , IFileService fileService 
            , ICategoryRepository categoryRepository , IBrandRepository brandRepository) : base(productRepository , fileService) {
            _productRepository = productRepository;
            _fileService = fileService;
            _categoryRepository = categoryRepository;
           _brandRepository = brandRepository;
        }

        public async Task<int> CreateWithImage(ProductRequestDTO request)
        {
            if (await _categoryRepository.GetByIdAsync(request.CategoryId) is null)
                  return 0;

            if (request.BrandId is int brandId && await _brandRepository.GetByIdAsync(brandId) is null)
                return 0;
            return await base.CreateAsync(request, true, "product" , true);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetProductAsync(id);
            if (product == null)
                return false;
            if (!string.IsNullOrEmpty(product.MainImagePublicId))
                await _fileService.DeleteAsync(product.MainImagePublicId);
            foreach (var img in product.ProductImages)
            {
                if (!string.IsNullOrEmpty(img.ImagePublicId))
                    await _fileService.DeleteAsync(img.ImagePublicId);
            }
            var result = await _productRepository.DeleteProductAsync(id);
            return result;
        }


        public async Task<List<ProductResponseDTO>> GetAllProductAsync(int pageNumber = 1, int pageSize = 1, bool onlyActive = false)
        {
            var products = await _productRepository.GetAllProductsWithImagesAsync();

            if (onlyActive)
                products = products.Where(p => p.Status == Status.Active).ToList();

            var pagedProducts = products.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return pagedProducts.Select(p=> new ProductResponseDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                Discount = p.Discount,
                Quantity = p.Quantity,
                Rate = p.Rate,
                MainImage = p.MainImage,
                CategoryId = p.CategoryId,
                BrandId = p.BrandId,
                SubImages = p.ProductImages?.Select(pi => pi.ImageUrl).ToList() ?? new List<string>(),
                Reviews = p.Reviews.Select(r => new ReviewResponseDTO
                {
                    Id = r.Id,
                    Rate = r.Rate,
                    Comment = r.Comment,
                    UserId = r.UserId,
                    FullName = r.User.FullName

                }).ToList(),
            }).ToList();
        }

        public async Task<ProductResponseDTO> GetProductByIdAsync(int id)
        {
            var product = await _productRepository.GetProductAsync(id);

            if (product == null)
                return null;

            return new ProductResponseDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Discount = product.Discount,
                Quantity = product.Quantity,
                Rate = product.Rate,
                MainImage = product.MainImage,
                CategoryId = product.CategoryId,
                BrandId = product.BrandId,
                SubImages = product.ProductImages?.Select(pi => pi.ImageUrl).ToList() ?? new List<string>(),
                Reviews = product.Reviews?.Select(r => new ReviewResponseDTO
                {
                    Id = r.Id,
                    Rate = r.Rate,
                    Comment = r.Comment,
                    UserId = r.UserId,
                    FullName = r.User.FullName
                }).ToList() ?? new List<ReviewResponseDTO>()
            };
        }

    }
}
