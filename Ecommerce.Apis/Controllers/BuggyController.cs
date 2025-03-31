using Ecommerce.Apis.DTOs;
using Ecommerce.Apis.Errors;
using Ecommerce.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// just to test the bugs not for production

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

        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
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

        #endregion NotFound

        #region ServerError

        // api/Buggy/servererror
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status500InternalServerError)]
        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var product = _dbContext.Products.Find(100);

            // any operation to throw null reference excpetion
            var productDto = product.ToString();

            return Ok(productDto);
        }

        #endregion ServerError

        #region BadRequest

        // api/Buggy/badrequest
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }

        #endregion BadRequest

        #region UnAuthorized

        // api/Buggy/unauthorized
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        [HttpGet("unauthorized")]
        public ActionResult GetUnAuthorized()
        {
            return Unauthorized(new ApiResponse(401));
        }

        #endregion UnAuthorized

        #region Validation Error

        // api/Buggy/GetValidationBadRequest/five
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [HttpGet("getvalidationbadrequest/{id}")]
        public ActionResult GetValidationBadRequest( int id )
        {
            return Ok();
        }

        #endregion Validation Error

    }
}