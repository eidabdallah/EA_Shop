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
    public interface IGenericService<TRequest , TResponse , TEntity>
    {
        int Create(TRequest request);
        IEnumerable<TResponse> GetAll(bool onlyActive = false);
        TResponse? GetById(int id);
        int Update(int id, TRequest request);
        int Delete(int id);
        bool ToggleStatus(int id);
    }
}
