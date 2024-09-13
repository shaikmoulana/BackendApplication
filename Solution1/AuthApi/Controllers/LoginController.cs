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
        //[HttpGet("GetToken")]
        public async Task<IActionResult> GetToken(string emailId, string password)
        {
            string token = await _tokenGeneration.Validate(emailId, password);
            if (!string.IsNullOrEmpty(token))
            {
                return Ok(token);
            }
            else
            {
                return Unauthorized("Invalid emailId or password.");
            }
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]  // Both User and Admin can access
        public IActionResult Hello()
        {
            return Ok("Hello User");
        }

        [HttpPost("admin")]
        [Authorize(Roles = "Admin")]  // Only Admin can perform this operation
        public IActionResult AdminOperation()
        {
            return Ok("This is an admin operation.");
        }

    }
}






