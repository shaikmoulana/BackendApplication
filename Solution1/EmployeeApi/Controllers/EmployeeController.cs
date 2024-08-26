
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
        public async Task<ActionResult<IEnumerable<Employee>>> GetAll()
        {
            _logger.LogInformation("Fetching all employeeTechnology");
            var employee = await _employeeService.GetAll();
            return Ok(employee);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> Get(string id)
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
            var employee = new Employee
            {
                Name = empDto.Name,
                DesignationId = empDto.DesignationId,
                EmployeeID = empDto.EmployeeID,
                EmailId = empDto.EmailId,
                DepartmentId = empDto.DepartmentId,
                ReportingTo = empDto.ReportingTo,
                JoiningDate = empDto.JoiningDate,
                RelievingDate = empDto.RelievingDate,
                Projection = empDto.Projection,
                IsActive = empDto.IsActive,
                CreatedBy = empDto.CreatedBy,
                CreatedDate = empDto.CreatedDate,
                UpdatedBy = empDto.UpdatedBy,
                UpdatedDate = empDto.UpdatedDate,
                Password = PasswordHasher.HashPassword(empDto.Password) // Hash the password
            };

            try
            {
                await _employeeService.Add(employee);
                return Ok(employee);
            }
            catch (ArgumentException ex)
            {
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

            var existingEmployee = await _employeeService.Get(id);
            if (existingEmployee == null)
            {
                _logger.LogWarning("Employee with id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updating employee with id: {Id}", id);

            existingEmployee.Name = empDto.Name;
            existingEmployee.DepartmentId = empDto.DepartmentId;
            existingEmployee.IsActive = empDto.IsActive;
            existingEmployee.UpdatedBy = empDto.UpdatedBy;
            existingEmployee.UpdatedDate = empDto.UpdatedDate;
            existingEmployee.Password = PasswordHasher.HashPassword(empDto.Password); // Hash the password

            await _employeeService.Update(existingEmployee);

            return Ok(existingEmployee);
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