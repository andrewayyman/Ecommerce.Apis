
using Ecommerce.Repository.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Apis
{
    public class Program
    {
        public static void Main( string[] args )
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Services
            // Add services to the container.

            // Db Connection 
            builder.Services.AddDbContext<StoreContext>( options =>
            {
                options.UseSqlServer( builder.Configuration.GetConnectionString("DefaultConnection") );


            });




            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); 
            #endregion

            var app = builder.Build();

            #region Configure Middlewares
            // Configure the HTTP request pipeline.
            if ( app.Environment.IsDevelopment() )
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers(); 
            #endregion

            app.Run();

        }
    }
}
