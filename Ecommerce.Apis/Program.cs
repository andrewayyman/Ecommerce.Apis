using Ecommerce.Apis.Errors;
using Ecommerce.Apis.Helpers;
using Ecommerce.Core.Entites;
using Ecommerce.Core.Repository.Contract;
using Ecommerce.Repository;
using Ecommerce.Repository.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Apis
{
    public class Program
    {
        public static async Task Main( string[] args )
        {

            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.


            // Db Connection 
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            // DI
            ///builder.Services.AddScoped< IGenericRepository<ProductBrand>, GenericRepository<ProductBrand> >();
            ///builder.Services.AddScoped< IGenericRepository<Product>, GenericRepository<Product> >();
            ///builder.Services.AddScoped< IGenericRepository<ProductCategory>, GenericRepository<ProductCategory> >();
            /// -------------- Replace the 3 ------------- //             
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            // AutoMapper 
            builder.Services.AddAutoMapper(typeof(MappingProfile));



            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            ////Handling Validation Error Response
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ( actionContext ) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                         .Select(e => e.ErrorMessage)
                                                         .ToList();

                    var response = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(response);
                };
            });

            #endregion

            var app = builder.Build();

            #region Configure Middlewares

            #region Update Database When App Runs
            // Ask Explicitly for creating object from storecontext
            using var scope = app.Services.CreateScope(); // AddScoped
            var services = scope.ServiceProvider;
            var _dbContext = services.GetRequiredService<StoreContext>();

            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                await _dbContext.Database.MigrateAsync();      // Update Database
                await StoreContextSeed.SeedAsync(_dbContext);  // Seed Data
            }
            catch ( Exception ex )
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error occurred during Migration");

            }
            #endregion



            // Configure the HTTP request pipeline.
            if ( app.Environment.IsDevelopment() )
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseStatusCodePagesWithRedirects("/Errors");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseStaticFiles(); // to get files from wwwroot
            app.MapControllers();
            #endregion

            app.Run();

        }
    }
}
