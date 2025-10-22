using EA_Ecommerce.BLL.Services.User;
using EA_Ecommerce.DAL.DTO.Requests.Block;
using EA_Ecommerce.DAL.DTO.Requests.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EA_Ecommerce.PL.Areas.Admin.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet("")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }
        [HttpPatch("block/{id}")]
        public async Task<IActionResult> BlockUser([FromRoute] string id, [FromBody] BlockRequestDTO request)
        {
            var result = await _userService.BlockUserAsync(id, request.days);
            if (!result)
            {
                return NotFound();
            }
            return Ok(new {message = "blocked user successfully"});
        }
        [HttpPatch("unblock/{id}")]
        public async Task<IActionResult> UnblockUser([FromRoute] string id)
        {
            var result = await _userService.UnBlockUserAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(new { message = "UnBlocked user successfully" });
        }
        [HttpPatch("isBlock/{id}")]
        public async Task<IActionResult> IsBlockUser([FromRoute] string id)
        {
            var isBlocked = await _userService.IsBlockedAsync(id);
            return Ok(isBlocked);
        }
        [Authorize(Roles = "SuperAdmin")]
        [HttpPatch("ChangeRole/{id}")]
        public async Task<IActionResult> ChangeUserRole([FromRoute] string id, [FromBody] ChangeRoleRequestDTO request)
        {
            var result = await _userService.ChangeUserRoleAsync(id, request.Role);
            return Ok(new { message = "Change role successfully" });
        }
    }
}
