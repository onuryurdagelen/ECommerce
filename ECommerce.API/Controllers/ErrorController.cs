using ECommerce.Data.Data;
using ECommerce.Data.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.API.Controllers
{
    [Route("errors/{code}")]
    public class ErrorController : BaseApiController
    {

        [HttpGet]
        public IActionResult Error(int code)
        {
            return new ObjectResult(new ApiResponse(code));
        }

      
    }
}
