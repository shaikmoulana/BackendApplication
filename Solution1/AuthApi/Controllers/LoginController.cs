using AuthApi.Helpers;
using DataServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly TokenGeneration _tokenGeneration;
        public LoginController(TokenGeneration tokenGeneration)
        {
            _tokenGeneration = tokenGeneration;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeLogin request)
        {
            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Username and password are required.");
            }

            string token = await _tokenGeneration.Validate(request.Username, request.Password);
            if (!string.IsNullOrEmpty(token))
            {
                return Ok(token);
            }
            else
            {
                return Unauthorized("Invalid username or password.");
            }
        }
        [HttpGet]
        [Authorize]  // Add roles or policies if required
        public IActionResult Hello()
        {
            return Ok("Hello User");
        }
    }
}






