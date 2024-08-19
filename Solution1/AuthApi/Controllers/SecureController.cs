using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecureController : ControllerBase
    {
        [HttpGet("secure-data")]
        [Authorize(Roles = "Admin")]  // Add roles or policies if required
        public IActionResult GetSecureData()
        {
            return Ok("This is a secure endpoint accessible by users.");
        }
    }
}
