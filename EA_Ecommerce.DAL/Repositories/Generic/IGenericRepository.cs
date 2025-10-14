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
        int Create(T entity);
        IEnumerable<T> GetAll(bool withTracking = false);
        T? GetById(int id);
        int Update(T entity);
        int Delete(T entity);
    }
}
