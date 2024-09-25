using DataServices.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTechnologyController : ControllerBase
    {
        private readonly IEmployeeTechnologyService _Service;
        private readonly ILogger<EmployeeTechnologyController> _logger;

        public EmployeeTechnologyController(IEmployeeTechnologyService Service, ILogger<EmployeeTechnologyController> logger)
        {
            _Service = Service;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead, Team Member")]
        public async Task<ActionResult<IEnumerable<EmployeeTechnologyDTO>>> GetAll()
        {
            _logger.LogInformation("Fetching all employeeTechnologies");
            var employeeTechnologies = await _Service.GetAll();
            if (User.IsInRole("Admin"))
            {
                return Ok(employeeTechnologies); // Admin can see all data
            }
            else
            {
                return Ok(employeeTechnologies.Where(d => d.IsActive)); // Non-admins see only active data
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead, Team Member")]
        public async Task<ActionResult<EmployeeTechnologyDTO>> Get(string id)
        {
            _logger.LogInformation("Fetching employee with id: {Id}", id);
            var employeeTechnology = await _Service.Get(id);

            if (employeeTechnology == null)
            {
                _logger.LogWarning("Employee with id: {Id} not found", id);
                return NotFound();
            }

            // Check if the logged-in user has the "Admin" role
            if (User.IsInRole("Admin"))
            {
                return Ok(employeeTechnology); // Admin can see both active and inactive 
            }
            else if (employeeTechnology.IsActive)
            {
                return Ok(employeeTechnology); // Non-admins can only see active data
            }
            else
            {
                _logger.LogWarning("EmployeeTechnology with id: {Id} is inactive and user does not have admin privileges", id);
                return Forbid(); // Return forbidden if non-admin tries to access an inactive 
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Director, Project Manager")]
        public async Task<IActionResult> Create(EmployeeTechnologyDTO empTechDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating Employee");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new Employee");

            try
            {
                var createdEmployeeTechnology = await _Service.Add(empTechDto);
                return CreatedAtAction(nameof(Get), new { id = createdEmployeeTechnology.Id }, createdEmployeeTechnology);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead")]
        public async Task<IActionResult> Update(string id, [FromBody] EmployeeTechnologyDTO empTechDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating employeeTechnology");
                return BadRequest(ModelState);
            }

            if (id != empTechDto.Id)
            {
                _logger.LogWarning("EmployeeTechnology id: {Id} does not match with the id in the request body", id);
                return BadRequest("EmployeeTechnology ID mismatch.");
            }

            try
            {
                await _Service.Update(empTechDto);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting employeeTechnology with id: {Id}", id);
            var success = await _Service.Delete(id);

            if (!success)
            {
                _logger.LogWarning("EmployeeTechnology with id: {Id} not found", id);
                return NotFound();
            }
            return NoContent();
        }
    }
}