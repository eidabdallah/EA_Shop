using EA_Ecommerce.BLL.Services.Categories;
using EA_Ecommerce.DAL.DTO.Requests.Category;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EA_Ecommerce.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IBrandService categoryService;

        public CategoriesController(IBrandService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            return Ok(categoryService.GetAllCategories());
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var category = categoryService.GetCategoryById(id);
            if (category == null) return NotFound(new { message = "Category not found" });
            return Ok(category);
        }
        [HttpPost]
        public IActionResult CreateCategory([FromBody] CategoryRequestDTO request)
        {
            int id = categoryService.CreateCategory(request);
            return CreatedAtAction(nameof(GetById), new { id = id } , new { message = "Category created successfully" }); // CreatedAtAction : 201 + Location header
        }
        [HttpPut("{id}")]
        public IActionResult UpdateCategory([FromRoute] int id, [FromBody] CategoryRequestDTO request)
        {
            var Updated = categoryService.UpdateCategory(id, request);
            return Updated > 0 ? Ok(new {message = "category updated"}) : NotFound(new { message = "Category not found" });
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteCategory([FromRoute] int id)
        {
            var Deleted = categoryService.DeleteCategory(id);
            return Deleted > 0 ? Ok(new { message = "category deleted" }) : NotFound(new { message = "Category not found" });
        }
        [HttpPatch("ToggleStatus/{id}")]
        public IActionResult ToggleCategoryStatus([FromRoute] int id)
        {
            var toggled = categoryService.ToggleStatus(id);
            return toggled == true  ? Ok(new { message = "category status toggled" }) : NotFound(new { message = "Category not found" });
        }
    }
}
