using EA_Ecommerce.BLL.Services.Categories;
using EA_Ecommerce.DAL.DTO.Requests.Category;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EA_Ecommerce.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound(new { message = "Category not found" });
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryRequestDTO request)
        {
            int id = await _categoryService.CreateAsync(request , true, "Category");
            return CreatedAtAction(nameof(GetById), new { id = id }, new { message = "Category created successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, [FromBody] CategoryRequestDTO request)
        {
            var updated = await _categoryService.UpdateAsync(id, request);
            return updated > 0 ? Ok(new { message = "Category updated" }) : NotFound(new { message = "Category not found" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            return deleted > 0 ? Ok(new { message = "Category deleted" }) : NotFound(new { message = "Category not found" });
        }

        [HttpPatch("ToggleStatus/{id}")]
        public async Task<IActionResult> ToggleStatus([FromRoute] int id)
        {
            var toggled = await _categoryService.ToggleStatusAsync(id);
            return toggled ? Ok(new { message = "Category status toggled" }) : NotFound(new { message = "Category not found" });
        }
    }
}
