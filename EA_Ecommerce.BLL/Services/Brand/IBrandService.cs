using EA_Ecommerce.DAL.DTO.Requests.Brand;
using EA_Ecommerce.DAL.DTO.Requests.Product;
using EA_Ecommerce.DAL.DTO.Responses.Brand;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Brand
{
    public interface IBrandService : IGenericService<BrandRequestDTO , BrandResponseDTO , DAL.Models.Brand>
    {
        Task<int> CreateWithImage(BrandRequestDTO request);

    }
}
