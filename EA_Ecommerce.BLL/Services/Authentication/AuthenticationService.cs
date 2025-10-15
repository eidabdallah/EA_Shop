using EA_Ecommerce.DAL.DTO.Requests.Login;
using EA_Ecommerce.DAL.DTO.Requests.RegisterRequestDTO;
using EA_Ecommerce.DAL.DTO.Responses.User;
using EA_Ecommerce.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EA_Ecommerce.BLL.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public AuthenticationService(UserManager<ApplicationUser> userManager , IConfiguration configuration , IEmailSender emailSender)
        {
            _userManager = userManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }
        public async Task<string> ConfirmEmail(string token , string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                throw new Exception("user not found");
            }
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return "Email Confirmed Successfully";
            }   
            return "Error while confirming your email";
            
        }
        public async Task<UserResponseDTO> RegisterAsync(RegisterRequestDTO RegisterRequest)
        {
            var user = new ApplicationUser()
            {
                FullName = RegisterRequest.FullName,
                Email = RegisterRequest.Email,
                UserName = RegisterRequest.UserName,
                PhoneNumber = RegisterRequest.PhoneNumber
            };
            var result =  await _userManager.CreateAsync(user, RegisterRequest.Password);
            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var escapedToken = Uri.EscapeDataString(token);
                var emailUrl = $"https://localhost:7169/api/Identity/Account/ConfirmEmail?token={escapedToken}&userId={user.Id}";
                await _emailSender.SendEmailAsync(RegisterRequest.Email, "Confirm your email",
                    $"<h1>hello ya {user.UserName} ❤️</h1>" + $"<a href='{emailUrl}'>Confirm</a>");
                return new UserResponseDTO()
                {
                    Token = RegisterRequest.Email
                };
            }else{
                throw new Exception($"{result.Errors}");
            }
        }
        public async Task<UserResponseDTO> LoginAsync(LoginRequestDTO loginRequest)
        {
           var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user is null)
            {
                throw new Exception("Invalid Email or Password");
            }
            if(!await _userManager.IsEmailConfirmedAsync(user))
            {
                throw new Exception("Please Confirm your Email");
            }
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if(!isPasswordValid)
            {
                throw new Exception("Invalid Email or Password");
            }
            return new UserResponseDTO()
            {
                Token = await CreateTokenAsync(user)
            };
        }
        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            // peyload : Data that is encoded in the token
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            //  user roles : 
            var roles = await _userManager.GetRolesAsync(user);
            // add roles to claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("jwtOptions")["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // create the token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }



    }
}
