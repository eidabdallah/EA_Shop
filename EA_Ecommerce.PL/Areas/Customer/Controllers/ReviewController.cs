using EA_Ecommerce.BLL.Services.Reviews;
using EA_Ecommerce.DAL.DTO.Requests.Reviews;
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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddReview([FromBody] ReviewRequestDTO request)
        {
            // user id form token
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _reviewService.AddReviewAsync(request, userId!);
            if (!result)
            {
                return BadRequest(new { Message = "You can only review products you have purchased and not reviewed before." });
            }
            return Ok(new { Message = "Review added successfully"});
        }
    }
}
