using EA_Ecommerce.BLL.Services.Products;
using EA_Ecommerce.DAL.DTO.Requests.Product;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EA_Ecommerce.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost("")]
        public async Task<IActionResult> Create([FromForm] ProductRequestDTO request)
        {
            var result = await _productService.CreateWithImage(request);
            if(result == 0)
            {
                return BadRequest(new { Message = "Invalid Category or Brand Id" });
            }
            return Ok(new { Message = "Product added successfully"});
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllCategories([FromQuery] int pageNumber = 1 , [FromQuery] int pageSize = 5)
        {
            var result = await _productService.GetAllProductAsync(pageNumber , pageSize , false);
            return Ok(new { Message = "Fetch Product successfully" , result });
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById([FromRoute] int id)
        {
            var result = await _productService.GetProductByIdAsync(id);
            if(result == null)
            {
                return NotFound(new { Message = "Product not found" });
            }
            return Ok(new { Message = "Fetch Product successfully", result });
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if(!result)
            {
                return NotFound(new { Message = "Product not found" });
            }
            return Ok(new { Message = "Product deleted successfully" });
        }
    }
}
