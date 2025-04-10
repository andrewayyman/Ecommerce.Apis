using Ecommerce.Apis.Errors;
using Ecommerce.Apis.Extensions;
using Ecommerce.Apis.Helpers;
using Ecommerce.Apis.Middleware;
using Ecommerce.Core.Entites;
using Ecommerce.Core.Entites.Identity;
using Ecommerce.Core.Repository.Contract;
using Ecommerce.Repository;
using Ecommerce.Repository.Data;
using Ecommerce.Repository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

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

            // SqlDb Connection
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Redis Connection
            builder.Services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var connection = builder.Configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(connection);
            });

            // Identity sqlDb Connection
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            // Now AddApplicationServices include all services as Extension Method
            // to clean up the this file we move the code in another class as extension method and call it here
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices();

            #endregion Configure Services

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

                var IdentityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbContext.Database.MigrateAsync(); // Update Identity Database
                var UserManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(UserManager);
                await StoreContextSeed.SeedAsync(_dbContext);  // Seed Data
            }
            catch ( Exception ex )
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "Error occurred during Migration");
            }

            #endregion Update Database When App Runs

            app.UseMiddleware<ExceptionMiddleware>(); // Custome Middleware for handling server error

            // Configure the HTTP request pipeline.
            if ( app.Environment.IsDevelopment() )
            {
                app.UseSwaggerMiddleware();               // extension method in ApplicationServicesExtension.cs
            }

            app.UseStatusCodePagesWithReExecute("/Errors/{0}"); // redirect to error page if status code is not 200
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseStaticFiles(); // to get files from wwwroot
            app.MapControllers();

            #endregion Configure Middlewares

            app.Run();
        }
    }
}