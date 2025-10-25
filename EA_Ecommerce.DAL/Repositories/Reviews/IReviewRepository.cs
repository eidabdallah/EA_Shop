using EA_Ecommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Reviews
{
    public interface IReviewRepository
    {
        Task AddReviewAsync(string userId , Review request);
        Task<bool> UserHasReviewedProductAsync(string userId, int productId);
    }
}
