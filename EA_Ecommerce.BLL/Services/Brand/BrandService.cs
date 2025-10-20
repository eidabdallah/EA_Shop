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
        public BrandService(IBrandRepository brandRepository , IFileService fileService) : base(brandRepository , fileService) {}
    }
}
