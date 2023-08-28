using ECommerce.Data.Exceptions;
using ECommerce.Data.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : BaseApiController
    {

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "secret stuff";
        }


        [HttpGet("notFound")]
        public ActionResult GetNotFoundRequest()
        {
            return NotFound(new ApiResponse(404));
        }
        [HttpGet("serverError")]

        public ActionResult GetServerError()
        {
            throw new Exception("This is a server error");
        }
        [HttpGet("badRequest")]
        public ActionResult GetBadRequest(string message = null)
        {
            return BadRequest(new ApiResponse(400, message));
        }
        [HttpGet("badRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return BadRequest(new ApiResponse(400));
        }
        [HttpGet("unAuthorized")]
        public ActionResult GetUnAuthorizedErrorRequest()
        {
            return Unauthorized(new ApiResponse(401));
        }
    }
}
