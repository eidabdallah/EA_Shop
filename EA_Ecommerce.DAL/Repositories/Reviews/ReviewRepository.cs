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

        public async Task<bool> DeleteReviewAsync(int reviewId, string userId)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId && r.UserId == userId);
            if (review is null)
                return false; 
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateReviewAsync(int reviewId, string userId, Review request)
        {
            var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == reviewId && r.UserId == userId);
            if(review is null)return false;
            review.Rate = request.Rate;
            if(request.Comment is not null)
                review.Comment = request.Comment;
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UserHasReviewedProductAsync(string userId, int productId)
        {
            return await _context.Reviews.AnyAsync(r=> r.UserId == userId && r.ProductId == productId);
        }
    }
}
