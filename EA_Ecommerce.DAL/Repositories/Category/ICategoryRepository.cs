using EA_Ecommerce.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Categories
{
    public interface ICategoryRepository
    {
        int Create(Category category);
        IEnumerable<Category> GetAll(bool withTracking = false);
        Category? GetById(int id);
        int Update(Category category);
        int Delete(Category category);
    }
}
