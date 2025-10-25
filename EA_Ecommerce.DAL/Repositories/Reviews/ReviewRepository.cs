using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.DAL.Repositories.Reviews
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddReviewAsync(string userId, Review request)
        {
            request.UserId = userId;
            request.ReviewDate = DateTime.UtcNow;
            await _context.Reviews.AddAsync(request);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> UserHasReviewedProductAsync(string userId, int productId)
        {
            return await _context.Reviews.AnyAsync(r=> r.UserId == userId && r.ProductId == productId);
        }
    }
}
