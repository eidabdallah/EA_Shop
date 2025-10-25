using EA_Ecommerce.DAL.DTO.Requests.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Reviews
{
    public interface IReviewService
    {
        Task<bool> AddReviewAsync(ReviewRequestDTO request , string UserId);
    }
}
