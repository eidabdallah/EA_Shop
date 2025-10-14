using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context) {
            this.context = context;
        }
        public int Create(Category category)
        {
            this.context.Categories.Add(category);
            return this.context.SaveChanges(); // saveChanges returns number of affected rows
        }

        public int Delete(Category category)
        {
           this.context.Categories.Remove(category);
           return this.context.SaveChanges();
        }

        public IEnumerable<Category> GetAll(bool withTracking = false)
        {
            if(withTracking)
                return this.context.Categories.ToList();
            return this.context.Categories.AsNoTracking().ToList();
        }

        public Category? GetById(int id) => this.context.Categories.Find(id);
        

        public int Update(Category category)
        {
            this.context.Categories.Update(category);
            return this.context.SaveChanges();
        }
    }
}
