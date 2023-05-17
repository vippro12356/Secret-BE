using Microsoft.AspNetCore.Mvc;
using Secrets_Sharing_BE.Models;

namespace Secrets_Sharing_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        [NonAction]
        public IActionResult Error(string message)
        {
            var result = new ErrorResponse
            {
                Message = message
            };
            return BadRequest(result);
        }
        public IActionResult Success(string message)
        {
            var result = new SuccessResponse<object>
            {
                Message = message
            };
            return Ok(result);
        }
        public IActionResult Success(object obj,string message = "")
        {
            var result = new SuccessResponse<object>
            {
                Message = message,
                Data = obj
            };
            return Ok(result);
        }
    }
}
