using EA_Ecommerce.DAL.DTO.Requests.Category;
using EA_Ecommerce.DAL.DTO.Responses.Category;
using EA_Ecommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Generic
{
    public interface IGenericService<TRequest, TResponse, TEntity>
    {
        Task<int> CreateAsync(TRequest request , bool WithImage = true, string? fileName = null, bool WithMultipleImage = false);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<TResponse>> GetAllAsync(bool onlyActive = false);
        Task<TResponse?> GetByIdAsync(int id);
        Task<bool> ToggleStatusAsync(int id);
        Task<int> UpdateAsync(int id, TRequest request, string? fileName = null);
    }

}
