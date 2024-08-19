
using DataServices.Models;
using EmployeeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeTechnologyController : ControllerBase
    {
        private readonly IEmployeeTechnologyService _employeeTechnologyService;
        private readonly ILogger<EmployeeTechnologyController> _logger;
        public EmployeeTechnologyController(IEmployeeTechnologyService employeeTechnologyService, ILogger<EmployeeTechnologyController> logger)
        {
            _employeeTechnologyService = employeeTechnologyService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<EmployeeTechnology>> GetAll()
        {
            _logger.LogInformation("Fetching all employeeTechnology");
            var employeeTechnology = await _employeeTechnologyService.GetAll();
            return Ok(employeeTechnology);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeTechnology>> Get(string id)
        {
            _logger.LogInformation("Fetching employeeTechnology with id: {Id}", id);
            var employeeTechnology = await _employeeTechnologyService.Get(id);

            if (employeeTechnology == null)
            {
                _logger.LogWarning("EmployeeTechnology with id: {Id} not found", id);
                return NotFound();
            }
            return Ok(employeeTechnology);
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeTechnology>> Create([FromBody] EmployeeTechnologyDTO emptechDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating employeeTechnology");
                return BadRequest(ModelState);
            }
            _logger.LogInformation("Creating a new employeeTechnology");
            var employeeTechnology = new EmployeeTechnology
            {
                Name = emptechDto.Name,
                TechnologyId = emptechDto.TechnologyId,
                IsActive = emptechDto.IsActive,
                CreatedBy = emptechDto.CreatedBy,
                CreatedDate = emptechDto.CreatedDate,
                UpdatedBy = emptechDto.UpdatedBy,
                UpdatedDate = emptechDto.UpdatedDate
            };
            var createdEmployeeTechnology = await _employeeTechnologyService.Add(employeeTechnology);
            return CreatedAtAction(nameof(Get), new { id = createdEmployeeTechnology.Id }, createdEmployeeTechnology);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] EmployeeTechnologyDTO emptechDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating employeeTechnology");
                return BadRequest(ModelState);
            }

            if (id != emptechDto.Id)
            {
                _logger.LogWarning("EmployeeTechnology id: {Id} does not match with the id in the request body", id);
                return BadRequest("EmployeeTechnology ID mismatch.");
            }

            var existingEmployeeTechnology = await _employeeTechnologyService.Get(id);
            if (existingEmployeeTechnology == null)
            {
                _logger.LogWarning("EmployeeTechnology with id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updating employeeTechnology with id: {Id}", id);

            // Update properties from DTO
            existingEmployeeTechnology.Name = emptechDto.Name;
            existingEmployeeTechnology.TechnologyId = emptechDto.TechnologyId;
            existingEmployeeTechnology.IsActive = emptechDto.IsActive;
            existingEmployeeTechnology.UpdatedBy = emptechDto.UpdatedBy;
            existingEmployeeTechnology.UpdatedDate = emptechDto.UpdatedDate;

            await _employeeTechnologyService.Update(existingEmployeeTechnology);

            return Ok(existingEmployeeTechnology);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting employeeTechnology with id: {Id}", id);
            var success = await _employeeTechnologyService.Delete(id);

            if (!success)
            {
                _logger.LogWarning("EmployeeTechnology with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }

    }
}
