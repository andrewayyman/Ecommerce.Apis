using Ecommerce.Apis.Errors;
using Ecommerce.Apis.Helpers;
using Ecommerce.Core.Repository.Contract;
using Ecommerce.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Apis.Extensions
{
    public static class ApplicationServicesExtension
    {
        public static IServiceCollection AddApplicationServices( this IServiceCollection services )
        {
            /// DI
            ///builder.Services.AddScoped< IGenericRepository<ProductBrand>, GenericRepository<ProductBrand> >();
            ///builder.Services.AddScoped< IGenericRepository<Product>, GenericRepository<Product> >();
            ///builder.Services.AddScoped< IGenericRepository<ProductCategory>, GenericRepository<ProductCategory> >();
            /// -------------- Replace the 3 ------------- //

            // inject generic repo
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            // AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));

            // Handling Validation Error Response
            services.Configure<ApiBehaviorOptions>(options =>
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

            return services;
        }

        public static WebApplication UseSwaggerMiddleware( this WebApplication app )
        {
            // Configure the HTTP request pipeline.

            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}