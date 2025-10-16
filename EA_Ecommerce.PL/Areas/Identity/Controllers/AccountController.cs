using EA_Ecommerce.BLL.Services.Authentication;
using EA_Ecommerce.DAL.DTO.Requests.ForgetPassword;
using EA_Ecommerce.DAL.DTO.Requests.Login;
using EA_Ecommerce.DAL.DTO.Requests.RegisterRequestDTO;
using EA_Ecommerce.DAL.DTO.Requests.ResetPassword;
using EA_Ecommerce.DAL.DTO.Responses.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EA_Ecommerce.PL.Areas.Identity.Controllers
{
    [Route("api/[area]/[controller]")]
    [ApiController]
    [Area("Identity")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponseDTO>> Register([FromBody] RegisterRequestDTO registerRequest)
        {
            var result = await _authenticationService.RegisterAsync(registerRequest);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserResponseDTO>> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var result = await _authenticationService.LoginAsync(loginRequest);
            return Ok(result);
        }

        [HttpGet("ConfirmEmail")]
        public async Task<ActionResult<string>> ConfirmEmail([FromQuery] string token , [FromQuery] string userId)
        {
            var result = await _authenticationService.ConfirmEmail(token , userId);
            return Ok(result);
        }
        [HttpPost("forgot-password")]
        public async Task<ActionResult<string>> ForgotPassword([FromBody] ForgotPasswordRequestDTO request)
        {
            var result = await _authenticationService.ForgotPassword(request);
            return Ok(result);
        }
        [HttpPatch("reset-password")]
        public async Task<ActionResult<string>> ResetPassword([FromBody] ResetPasswordRequestDTO request)
        {
            var result = await _authenticationService.ResetPassword(request);
            return Ok(result);
        }

    }
}
