using EA_Ecommerce.DAL.DTO.Requests.ForgetPassword;
using EA_Ecommerce.DAL.DTO.Requests.Login;
using EA_Ecommerce.DAL.DTO.Requests.RegisterRequestDTO;
using EA_Ecommerce.DAL.DTO.Requests.ResetPassword;
using EA_Ecommerce.DAL.DTO.Responses.User;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<UserResponseDTO> LoginAsync(LoginRequestDTO loginRequest);
        Task<UserResponseDTO> RegisterAsync(RegisterRequestDTO RegisterRequest , HttpRequest request);
        Task<string> ConfirmEmail(string token, string userId);
        Task<bool> ForgotPassword(ForgotPasswordRequestDTO forgotPasswordRequest);
        Task<bool> ResetPassword(ResetPasswordRequestDTO resetPasswordRequest);


    }
}
