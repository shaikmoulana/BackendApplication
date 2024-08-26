using AuthApi.Helpers;
using Azure.Core;
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
        //[HttpGet]
        public async Task<IActionResult> Post(string username, string password)
        {
            string token = await _tokenGeneration.Validate(username, password);
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






