using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// for common endpoints and properties for all controllers

namespace Ecommerce.Apis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}