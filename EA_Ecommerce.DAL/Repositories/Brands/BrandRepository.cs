using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Brands
{
    public class BrandRepository : GenericRepository<Brand>, IBrandRepository{
        public BrandRepository(ApplicationDbContext context) : base(context) { }
    }
}
