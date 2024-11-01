using Ecommerce.Apis.Errors;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ecommerce.Apis.Middleware
{
    // By convention middleware , Name must end with Middleware 
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IWebHostEnvironment env
            )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync (HttpContext httpContext)
        {
            try
            {

                await _next.Invoke(httpContext);

            }
            catch ( Exception ex ) // polish the response
            {
                // log error 
                _logger.LogError(ex, ex.Message); // in Development

                // Content Type
                httpContext.Response.ContentType = "application/json";

                // status code
                httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError ;

                // Exception content , incase dev. return the full excpetion , any other case return excpetion with status code
                var response = _env.IsDevelopment() ?
                    new ApiExceptionResponse(( int )HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString())
                    : new ApiExceptionResponse(( int )HttpStatusCode.InternalServerError);

                // make the response camelcase 
                var options = new JsonSerializerOptions()
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                // Body
                 var json = JsonSerializer.Serialize(response , options);
                await httpContext.Response.WriteAsync( json ); // take json that's why we serialize it first





            }
        }




    }
}
