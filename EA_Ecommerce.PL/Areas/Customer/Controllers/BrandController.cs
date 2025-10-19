using EA_Ecommerce.BLL.Services.Brand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EA_Ecommerce.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }
        [HttpGet]
        public IActionResult GetAllBrand()
        {
            return Ok(_brandService.GetAllAsync(true));
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var category = _brandService.GetByIdAsync(id);
            if (category == null) return NotFound(new { message = "Category not found" });
            return Ok(category);
        }
    }
}
