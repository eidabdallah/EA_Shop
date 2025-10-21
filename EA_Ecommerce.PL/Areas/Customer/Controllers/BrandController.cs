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
        public async Task<IActionResult> GetAllBrand()
        {
            var brands = await _brandService.GetAllAsync(true);
            return Ok(brands);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var brand = await _brandService.GetByIdAsync(id);
            if (brand == null)
                return NotFound(new { message = "Brand not found" });
            return Ok(brand);
        }
    }
}
