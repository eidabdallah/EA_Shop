using CloudinaryDotNet;
using EA_Ecommerce.BLL.Configurations;
using EA_Ecommerce.BLL.Services.Authentication;
using EA_Ecommerce.BLL.Services.Brand;
using EA_Ecommerce.BLL.Services.Categories;
using EA_Ecommerce.BLL.Services.Files;
using EA_Ecommerce.BLL.Services.Products;
using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Brands;
using EA_Ecommerce.DAL.Repositories.Categories;
using EA_Ecommerce.DAL.Repositories.Products;
using EA_Ecommerce.DAL.utils.SeedData;
using EA_Ecommerce.PL.utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
namespace EA_Ecommerce.PL
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

            builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
            builder.Services.AddScoped<IBrandRepository, BrandRepository>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ISeedData, SeedData>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IEmailSender, EmailSetting>();
            builder.Services.AddScoped<IFileService, FileService>();


            builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
            builder.Services.AddSingleton<Cloudinary>(sp =>
            {
                var s = sp.GetRequiredService<IOptions<CloudinarySettings>>().Value;
                var account = new Account(s.CloudName, s.ApiKey, s.ApiSecret);
                return new Cloudinary(account);
            });


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
            {
                // validation for password
                option.Password.RequiredLength = 8;
                option.Password.RequireNonAlphanumeric = false;
                option.Password.RequireDigit = true;
                option.Password.RequireLowercase = true;
                option.Password.RequireUppercase = true;

                // validation For email
                option.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();



            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("jwtOptions")["SecretKey"]))
                };
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            var scope = app.Services.CreateScope();
            var objectOfSeddDtata = scope.ServiceProvider.GetRequiredService<ISeedData>();
            await objectOfSeddDtata.IdentityDataSeedingAsync();
            

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}
