using DataServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoleApi.Services;

namespace RoleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _Service;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService Service, ILogger<RoleController> logger)
        {
            _Service = Service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDTO>>> GetAll()
        {
            _logger.LogInformation("Fetching all Roles");
            var roles = await _Service.GetAll();
            return Ok(roles);
        }
    }
}
