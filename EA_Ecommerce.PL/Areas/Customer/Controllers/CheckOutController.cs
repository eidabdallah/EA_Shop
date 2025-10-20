using EA_Ecommerce.BLL.Services.CheckOut;
using EA_Ecommerce.DAL.DTO.Requests.CheckOut;
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
    public class CheckOutController : ControllerBase
    {
        private readonly ICheckOutService _checkOutService;

        public CheckOutController(ICheckOutService checkOutService)
        {
            _checkOutService = checkOutService;
        }
        [HttpPost("payment")]
        public async Task<IActionResult> Payment([FromBody] CheckOutRequestDTO request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _checkOutService.ProcessPaymentAsync(request, userId, Request);
            return Ok(response);
        }
        [HttpGet("success/{orderId}")]
        [AllowAnonymous]
        public async Task<IActionResult> Success([FromRoute] int orderId)
        {
            var result = await _checkOutService.HandlePaymentSuccessAsync(orderId);
            return Ok(result);
        }
        [HttpGet("cancel")]
        [AllowAnonymous]
        public IActionResult Error() { 
          return Ok("Payment canceled!");
        }
    }
}
