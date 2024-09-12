using DataServices.Data;
using DataServices.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly DataBaseContext _context;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, DataBaseContext context)
        {
            _employeeService = employeeService;
            _logger = logger;
            _context = context;
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

        /*[HttpPost]
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

        }*/
        /*[HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDTO employeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Save Employee details first
                var employee = new Employee
                {
                    Name = employeeDto.Name,
                    DesignationId = employeeDto.Designation,
                    EmployeeID = employeeDto.EmployeeID,
                    EmailId = employeeDto.EmailId,
                    DepartmentId = employeeDto.Department,
                    ReportingTo = employeeDto.ReportingTo,
                    JoiningDate = employeeDto.JoiningDate,
                    RelievingDate = employeeDto.RelievingDate,
                    Projection = employeeDto.Projection,
                    IsActive = employeeDto.IsActive,
                    CreatedBy = employeeDto.CreatedBy,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = employeeDto.UpdatedBy,
                    UpdatedDate = DateTime.Now,
                    Password = employeeDto.Password,
                    PhoneNo = employeeDto.PhoneNo,
                    //Role = employeeDto.Role,
                };

                await _context.TblEmployee.AddAsync(employee);
                await _context.SaveChangesAsync();

                // After saving the employee, save the technologies into EmployeeTechnology table
                if (employeeDto.Technology != null && employeeDto.Technology.Any())
                {
                    foreach (var technologyId in employeeDto.Technology)
                    {
                        var employeeTechnology = new EmployeeTechnology
                        {
                            EmployeeID = employee.Id,  // Use the newly created employee's ID
                            Technology = technologyId.ToString(),
                        };

                        _context.TblEmployeeTechnology.Add(employeeTechnology);
                    }

                    await _context.SaveChangesAsync();
                }

                return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }*/

        [HttpPost]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDTO employeeDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Find the Designation by Name (assuming Designation is a table)
                var designation = await _context.TblDesignation
                    .FirstOrDefaultAsync(d => d.Name == employeeDto.Designation); // Match the name from DTO

                if (designation == null)
                {
                    // If no matching designation is found, return an error response
                    return BadRequest(new { error = "Invalid designation name" });
                }

                var department = await _context.TblDepartment
                    .FirstOrDefaultAsync(d => d.Name == employeeDto.Department); // Match the name from DTO

                if (department == null)
                {
                    // If no matching designation is found, return an error response
                    return BadRequest(new { error = "Invalid department name" });
                }

                // Save Employee details first
                var employee = new Employee
                {
                    Name = employeeDto.Name,
                    DesignationId = designation.Id,  // Assign the found Designation ID
                    EmployeeID = employeeDto.EmployeeID,
                    EmailId = employeeDto.EmailId,
                    DepartmentId = department.Id,
                    ReportingTo = employeeDto.ReportingTo,
                    JoiningDate = employeeDto.JoiningDate,
                    RelievingDate = employeeDto.RelievingDate,
                    Projection = employeeDto.Projection,
                    IsActive = employeeDto.IsActive,
                    CreatedBy = employeeDto.CreatedBy,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = employeeDto.UpdatedBy,
                    UpdatedDate = DateTime.Now,
                    Password = employeeDto.Password,
                    PhoneNo = employeeDto.PhoneNo,
                };

                await _context.TblEmployee.AddAsync(employee);
                await _context.SaveChangesAsync();

                // After saving the employee, save the technologies into EmployeeTechnology table
                if (employeeDto.Technology != null && employeeDto.Technology.Any())
                {
                    foreach (var technologyId in employeeDto.Technology)
                    {
                        var employeeTechnology = new EmployeeTechnology
                        {
                            EmployeeID = employee.Id,  // Use the newly created employee's ID
                            Technology = technologyId.ToString(),
                        };

                        await _context.TblEmployeeTechnology.AddAsync(employeeTechnology);
                    }

                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                // Log the exception as needed
                return StatusCode(500, "Internal server error");
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