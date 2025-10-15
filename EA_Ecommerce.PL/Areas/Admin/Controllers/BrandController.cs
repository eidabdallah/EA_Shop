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
        public IActionResult GetAllBrand()
        {
            return Ok(_brandService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var category = _brandService.GetById(id);
            if (category == null) return NotFound(new { message = "Category not found" });
            return Ok(category);
        }
        [HttpPost]
        public IActionResult CreateCategory([FromBody] BrandRequestDTO request)
        {
            int id = _brandService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = id }, new { message = "Category created successfully" }); 
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromRoute] int id, [FromBody] BrandRequestDTO request)
        {
            var Updated = _brandService.Update(id, request);
            return Updated > 0 ? Ok(new { message = "category updated" }) : NotFound(new { message = "Category not found" });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory([FromRoute] int id)
        {
            var Deleted = _brandService.Delete(id);
            return Deleted > 0 ? Ok(new { message = "category deleted" }) : NotFound(new { message = "Category not found" });
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleCategoryStatus([FromRoute] int id)
        {
            var toggled = _brandService.ToggleStatus(id);
            return toggled == true ? Ok(new { message = "category status toggled" }) : NotFound(new { message = "Category not found" });
        }
    }
}
