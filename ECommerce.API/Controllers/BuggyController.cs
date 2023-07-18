using ECommerce.Data.Exceptions;
using ECommerce.Data.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseApiController
    {
        [HttpGet("notFound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(new ApiResponse(404));
        }
        [HttpGet("serverError")]

        public IActionResult GetServerError()
        {
            throw new Exception("This is a server error");
        }
        [HttpGet("badRequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("badRequest/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("unAuthorized")]
        public IActionResult GetUnAuthorizedErrorRequest()
        {
            return Unauthorized(new ApiResponse(401));
        }
    }
}
