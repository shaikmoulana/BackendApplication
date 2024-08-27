using DataServices.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger)
        {
            _employeeService = employeeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> GetAll()
        {
            _logger.LogInformation("Fetching all employees");
            var employees = await _employeeService.GetAll();
            return Ok(employees);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDTO>> Get(string id)
        {
            _logger.LogInformation("Fetching employee with id: {Id}", id);
            var employee = await _employeeService.Get(id);

            if (employee == null)
            {
                _logger.LogWarning("Employee with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDTO empDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating Employee");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new Employee");

            try
            {
                var createdEmployee = await _employeeService.Add(empDto);
                return CreatedAtAction(nameof(Get), new { id = createdEmployee.Id }, createdEmployee);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] EmployeeDTO empDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating employee");
                return BadRequest(ModelState);
            }

            if (id != empDto.Id)
            {
                _logger.LogWarning("Employee id: {Id} does not match with the id in the request body", id);
                return BadRequest("Employee ID mismatch.");
            }

            try
            {
                await _employeeService.Update(empDto);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting employee with id: {Id}", id);
            var success = await _employeeService.Delete(id);

            if (!success)
            {
                _logger.LogWarning("Employee with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}