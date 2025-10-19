using EA_Ecommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Generic
{
    public interface IGenericRepository<T>  where T : BaseModel
    {
        Task<int> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync(bool withTracking = false);
        Task<T?> GetByIdAsync(int id);
        Task<int> UpdateAsync(T entity);
        Task<int> DeleteAsync(T entity);
    }
}
