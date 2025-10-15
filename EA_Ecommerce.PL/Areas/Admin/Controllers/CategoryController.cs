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
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(categoryService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var category = categoryService.GetById(id);
            if (category == null) return NotFound(new { message = "Category not found" });
            return Ok(category);
        }
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryRequestDTO request)
        {
            int id = categoryService.Create(request);
            return CreatedAtAction(nameof(GetById), new { id = id }, new { message = "Category created successfully" }); // CreatedAtAction : 201 + Location header
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromRoute] int id, [FromBody] CategoryRequestDTO request)
        {
            var Updated = categoryService.Update(id, request);
            return Updated > 0 ? Ok(new { message = "category updated" }) : NotFound(new { message = "Category not found" });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory([FromRoute] int id)
        {
            var Deleted = categoryService.Delete(id);
            return Deleted > 0 ? Ok(new { message = "category deleted" }) : NotFound(new { message = "Category not found" });
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleCategoryStatus([FromRoute] int id)
        {
            var toggled = categoryService.ToggleStatus(id);
            return toggled == true ? Ok(new { message = "category status toggled" }) : NotFound(new { message = "Category not found" });
        }
    }
}
