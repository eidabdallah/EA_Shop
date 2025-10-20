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
            return await _cartRepository.Add(newItem) > 0;
        }

        public async Task<CartSummaryResponse> getCart(string UserId)
        {
            var cartItems = await _cartRepository.GetUserCart(UserId);
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
    }
}
