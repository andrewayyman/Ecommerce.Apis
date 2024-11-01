using Ecommerce.Apis.Errors;
using Ecommerce.Apis.Extensions;
using Ecommerce.Apis.Helpers;
using Ecommerce.Apis.Middleware;
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
            builder.Services.AddControllers();

            // Swagger Services as Extension
            builder.Services.AddSwaggerServices();

            // Db Connection 
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });


            // to clean up the this file we move the code in another class as extension method and call it here
            // Application Services as Extension
            builder.Services.AddApplicationServices();


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

            app.UseMiddleware<ExceptionMiddleware>(); // handling server error

            // Configure the HTTP request pipeline.
            if ( app.Environment.IsDevelopment() )
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();

                app.UseSwaggerMiddleware(); // extension method

            }   

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseStaticFiles(); // to get files from wwwroot
            app.MapControllers();


            #endregion

            app.Run();


        }
    }
}
