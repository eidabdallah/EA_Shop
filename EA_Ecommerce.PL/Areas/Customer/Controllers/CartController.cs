using EA_Ecommerce.BLL.Services.Carts;
using EA_Ecommerce.DAL.DTO.Requests.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EA_Ecommerce.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        [HttpPost("")]
        public async Task<IActionResult> AddToCart([FromBody] CartRequestDTO request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.AddToCart(request, userId);
            return result ? Ok(new { message = "Item added to cart successfully" })
                : BadRequest(new { message = "Failed to add item to cart" });
        }
        [HttpGet("")]
        public async Task<IActionResult> GetCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartService.getCart(userId);
            return Ok(cart);
        }
        [HttpDelete("")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.ClearCartAsync(userId!);
            return result ? Ok(new { message = "Cart cleared successfully" })
                : BadRequest(new { message = "Failed to clear cart" });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductFromCart([FromRoute] int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _cartService.DeleteProductFromCartAsync(id, userId!);
            return result ? Ok(new { message = "Product removed from cart successfully" })
                : BadRequest(new { message = "Failed to remove product from cart" });
        }
        [HttpPatch("update-count")]
        public async Task<IActionResult> UpdateProductCount([FromBody] CartCountRequestDTO request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(request.Operation) ||
                (request.Operation != "increase" && request.Operation != "decrease"))
            {
                return BadRequest(new { message = "Invalid operation type" });
            }

            var result = await _cartService.UpdateProductCountAsync(request, userId!);

            return result
                ? Ok(new { message = "Product count updated successfully" })
                : BadRequest(new { message = "Failed to update product count" });
        }

    }
}
