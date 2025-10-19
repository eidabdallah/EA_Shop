using EA_Ecommerce.BLL.Services.Files;
using EA_Ecommerce.DAL.DTO.Requests.Brand;
using EA_Ecommerce.DAL.DTO.Responses.Brand;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Brands;
using EA_Ecommerce.DAL.Repositories.Generic;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Brand
{
    public class BrandService : GenericService<BrandRequestDTO, BrandResponseDTO, DAL.Models.Brand> , IBrandService
    {
        private readonly IBrandRepository _brandRepository;
        private readonly IFileService _fileService;

        public BrandService(IBrandRepository brandRepository , IFileService fileService) : base(brandRepository)
        {
            _brandRepository = brandRepository;
            _fileService = fileService;
        }

        public async Task<int> CreateWithImage(BrandRequestDTO request)
        {
            var entity = request.Adapt<DAL.Models.Brand>();
            entity.CreatedAt = DateTime.UtcNow;


            if (request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                entity.MainImage = imagePath;
            }
            return await _brandRepository.CreateAsync(entity);
        }
    }
}
