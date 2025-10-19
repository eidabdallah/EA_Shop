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
            return Ok(result);
        }
        [HttpGet("")]
        public IActionResult GetAllCategories()
        {
            return Ok(_productService.GetAll());
        }
    }
}
