using EA_Ecommerce.DAL.DTO.Requests.Brand;
using EA_Ecommerce.DAL.DTO.Responses.Brand;
using EA_Ecommerce.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Brand
{
    public class BrandService : GenericService<BrandRequestDTO, BrandResponseDTO, DAL.Models.Brand> , IBrandService
    {
        public BrandService(IGenericRepository<DAL.Models.Brand> brandRepository) : base(brandRepository){}
    }
}
