using EA_Ecommerce.DAL.DTO.Requests.Reviews;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Order;
using EA_Ecommerce.DAL.Repositories.Reviews;
using Mapster;

namespace EA_Ecommerce.BLL.Services.Reviews
{
    public class ReviewService : IReviewService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IOrderRepository orderRepository , IReviewRepository reviewRepository) {
            _orderRepository = orderRepository;
            _reviewRepository = reviewRepository;
        }
        public async Task<bool> AddReviewAsync(ReviewRequestDTO request, string UserId)
        {
           var hasOrder = await _orderRepository.UserHasApprovedOrderForProductAsync(UserId, request.ProductId);
            if (!hasOrder) return false;

            var alreadyReviewed = await _reviewRepository.UserHasReviewedProductAsync(UserId, request.ProductId);
            if (alreadyReviewed) return false;

            var review = request.Adapt<Review>();
            await _reviewRepository.AddReviewAsync(UserId,review);
            return true;

        }
        public async Task<bool> DeleteReviewAsync(int reviewId, string userId)
        {
            return await _reviewRepository.DeleteReviewAsync(reviewId, userId);
        }
        public async Task<bool> UpdateReviewAsync(int reviewId, string userId, ReviewUpdateRequestDTO request)
        {
            var review = request.Adapt<Review>();
            return await _reviewRepository.UpdateReviewAsync(reviewId, userId, review);
        }
    }

}
