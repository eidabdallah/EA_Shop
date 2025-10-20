using EA_Ecommerce.BLL.Services.Brand;
using EA_Ecommerce.DAL.DTO.Requests.Brand;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EA_Ecommerce.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
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
            var brands = await _brandService.GetAllAsync();
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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] BrandRequestDTO request)
        {
            int id = await _brandService.CreateAsync(request , true , "Brand");
            return CreatedAtAction(nameof(GetById), new { id = id }, new { message = "Brand created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] BrandRequestDTO request)
        {
            var updated = await _brandService.UpdateAsync(id, request);
            return updated > 0 ? Ok(new { message = "Brand updated" }) : NotFound(new { message = "Brand not found" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var deleted = await _brandService.DeleteAsync(id);
            return deleted > 0 ? Ok(new { message = "Brand deleted" }) : NotFound(new { message = "Brand not found" });
        }

        [HttpPatch("ToggleStatus/{id}")]
        public async Task<IActionResult> ToggleStatus([FromRoute] int id)
        {
            var toggled = await _brandService.ToggleStatusAsync(id);
            return toggled ? Ok(new { message = "Brand status toggled" }) : NotFound(new { message = "Brand not found" });
        }

    }
}
