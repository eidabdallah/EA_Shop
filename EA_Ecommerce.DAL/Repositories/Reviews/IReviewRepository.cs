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
        Task <bool> DeleteReviewAsync(int reviewId , string userId);
        Task<bool> UpdateReviewAsync(int reviewId, string userId , Review request);
    }
}
