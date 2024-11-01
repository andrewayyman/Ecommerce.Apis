using Ecommerce.Apis.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Apis.Controllers
{
    [Route("Errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)] // not documented endpoint idon't need it to be shown in apidoc
    public class ErrorsController : ControllerBase
    {
        // called when you request to endpoint that not exist, not auth 

        public ActionResult Error( int code )
        {
            if ( code == 401 )
            {
                return Unauthorized( new ApiResponse(401) );

            }
            else if ( code == 404)
            {
                return NotFound(new ApiResponse(404));
            }
            else 
                return StatusCode(code);
        }

    }
}
