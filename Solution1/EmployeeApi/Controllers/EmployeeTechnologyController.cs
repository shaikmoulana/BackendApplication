using DataServices.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(employeeTechnologies);
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

            return Ok(employeeTechnology);
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

        [HttpDelete("{id}")]
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