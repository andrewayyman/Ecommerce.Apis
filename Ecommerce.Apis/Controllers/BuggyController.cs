using Ecommerce.Apis.Errors;
using Ecommerce.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly StoreContext _dbContext;

        public BuggyController( StoreContext dbContext )
        {
            _dbContext = dbContext;
        }


        #region NotFound

        // api/Buggy/notfound
        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var product = _dbContext.Products.Find(100);
            if ( product == null )
                if ( product == null )
                {
                    return NotFound(new ApiResponse(404));
                }

            return Ok(product);
        }
        #endregion

        #region ServerError

        // api/Buggy/servererror
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _dbContext.Products.Find(100);

            // any operation to throw null reference excpetion 
            var productDto = product.ToString();

            return Ok(productDto);
        }

        #endregion

        #region BadRequest 

        // api/Buggy/badrequest
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {   
            return BadRequest(new ApiResponse(400));
        }

        #endregion

        #region UnAuthorized


        // api/Buggy/unauthorized
        [HttpGet("unauthorized")]

        public ActionResult GetUnAuthorized()
        {
            return Unauthorized(new ApiResponse(401));
        }

        #endregion

        #region Validation Error

        // api/Buggy/GetValidationBadRequest/five
        [HttpGet("getvalidationbadrequest/{id}")]
        public ActionResult GetValidationBadRequest( int id )
        {
            return Ok();
        }


        #endregion  

    }
}
