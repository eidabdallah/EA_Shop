using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public int Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges(); // saveChanges returns number of affected rows
        }
        public int Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<T> GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _context.Set<T>().ToList();
            return _context.Set<T>().AsNoTracking().ToList();
        }

        public T? GetById(int id) => _context.Set<T>().Find(id);
        
        public int Update(T entity)
        {
           _context.Set<T>().Update(entity);
            return _context.SaveChanges();
        }
    }
}
