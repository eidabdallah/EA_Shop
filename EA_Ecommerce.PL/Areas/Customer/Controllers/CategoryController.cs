using EA_Ecommerce.BLL.Services.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EA_Ecommerce.PL.Areas.Customer.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Customer")]
    [Authorize(Roles = "Customer")]
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
            return Ok(categoryService.GetAll(true));
        }
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var category = categoryService.GetById(id);
            if (category == null) return NotFound(new { message = "Category not found" });
            return Ok(category);
        }
    }
}
