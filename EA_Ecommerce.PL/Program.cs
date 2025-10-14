using EA_Ecommerce.BLL.Services.Brand;
using EA_Ecommerce.BLL.Services.Categories;
using EA_Ecommerce.DAL.Data;
using EA_Ecommerce.DAL.Models;
using EA_Ecommerce.DAL.Repositories.Brands;
using EA_Ecommerce.DAL.Repositories.Categories;
using EA_Ecommerce.DAL.utils.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
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
            builder.Services.AddScoped<IBrandService, BrandService>();
            builder.Services.AddScoped<ISeedData, SeedData>();


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();



            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
           

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            var scope = app.Services.CreateScope();
            var objectOfSeddDtata = scope.ServiceProvider.GetRequiredService<ISeedData>();
            await objectOfSeddDtata.DataSeedingAsync();
            await objectOfSeddDtata.IdentityDataSeedingAsync();


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
