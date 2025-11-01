using EA_Ecommerce.DAL.DTO.Requests.Cart;
using EA_Ecommerce.DAL.DTO.Responses.Cart;
using EA_Ecommerce.DAL.Repositories.Carts;

namespace EA_Ecommerce.BLL.Services.Carts
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<bool> AddToCart(CartRequestDTO request, string UserId)
        {
            var newItem = new DAL.Models.Cart
            {
                ProductId = request.ProductId,
                Count = 1,
                UserId = UserId
            };
            return await _cartRepository.AddAsync(newItem) > 0;
        }

        public async Task<bool> ClearCartAsync(string UserId)
        {
            return await _cartRepository.ClearCartAsync(UserId);
        }

        public async Task<CartSummaryResponse> getCart(string UserId)
        {
            var cartItems = await _cartRepository.GetUserCartAsync(UserId);
            var cartResponse = new CartSummaryResponse
            {
                Items = cartItems.Select(item => new CartResponseDTO
                {
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Price = item.Product.Price,
                    Count = item.Count
                }).ToList()
            };
            return cartResponse;
        }
        public async Task<bool> DeleteProductFromCartAsync(int ProductId, string UserId)
        {
            return await _cartRepository.DeleteProductFromCartAsync(ProductId, UserId);
        }
        public async Task<bool> UpdateProductCountAsync(CartCountRequestDTO request, string UserId)
        {
           return await _cartRepository.UpdateProductCountAsync(request.ProductId, request.Operation, UserId);
        }
    }
}
