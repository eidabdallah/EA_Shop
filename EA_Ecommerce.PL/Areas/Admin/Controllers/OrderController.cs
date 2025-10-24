using EA_Ecommerce.BLL.Services.Order;
using EA_Ecommerce.DAL.DTO.Requests.Order;
using EA_Ecommerce.DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EA_Ecommerce.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetOrdersByStatus(OrderStatusEnum status)
        {
            var orders = await _orderService.GetOrdersByStatusAsync(status);
            return Ok(orders);
        }
        [HttpPatch("change-status/{orderId}")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, [FromBody] OrderRequestDTO request)
        {
            var result = await _orderService.ChangeStatusAsync(orderId, request.status);
            if (!result)
            {
                return NotFound(new { Message = "Order not found or status unchanged." });
            }
            return Ok(new { Message = "Order status updated successfully." });
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserOrders(string userId)
        {
            var order = await _orderService.GetAllOrdersByUserAsync(userId);
            if (order == null)
            {
                return NotFound(new { Message = "Order not found." });
            }
            return Ok(order);
        }
    }
}
