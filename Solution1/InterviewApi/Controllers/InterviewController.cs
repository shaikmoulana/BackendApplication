/*using DataServices.Models;
using InterviewApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterviewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewController : Controller
    {
        private readonly IInterviewService _service;
        private readonly ILogger<InterviewController> _logger;

        public InterviewController(IInterviewService Service, ILogger<InterviewController> logger)
        {
            _service = Service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interviews>>> GetAll()
        {
            _logger.LogInformation("Fetching all employeeTechnology");
            var data = await _service.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Interviews>> Get(string id)
        {
            _logger.LogInformation("Fetching employee with id: {Id}", id);
            var data = await _service.Get(id);

            if (data == null)
            {
                _logger.LogWarning("Employee with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Create(InterviewsDTO _object)
        {
            var data = new Interviews
            {
                // Map properties from DTO to entity
                SOWRequirement = _object.SOWRequirement,
                Name = _object.Name,
                InterviewDate = _object.InterviewDate,
                YearsOfExperience = _object.YearsOfExperience,
                Status = _object.Status,
                On_Boarding = _object.On_Boarding,
                Recruiter = _object.Recruiter,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate

            };

            var created = await _service.Create(data);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] InterviewsDTO _object)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating employee");
                return BadRequest(ModelState);
            }

            if (id != _object.Id)
            {
                _logger.LogWarning("Employee id: {Id} does not match with the id in the request body", id);
                return BadRequest("Employee ID mismatch.");
            }

            var existingData = await _service.Get(id);
            if (existingData == null)
            {
                _logger.LogWarning("Employee with id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updating employee with id: {Id}", id);

            // Update properties from DTO
            existingData.SOWRequirement = _object.SOWRequirement;
            existingData.Name = _object.Name;
            existingData.InterviewDate = _object.InterviewDate;
            existingData.YearsOfExperience = _object.YearsOfExperience;
            existingData.Status = _object.Status;
            existingData.On_Boarding = _object.On_Boarding;
            existingData.Recruiter = _object.Recruiter;
            existingData.IsActive = _object.IsActive;
            existingData.CreatedBy = _object.CreatedBy;
            existingData.CreatedDate = _object.CreatedDate;
            existingData.UpdatedBy = _object.UpdatedBy;
            existingData.UpdatedDate = _object.UpdatedDate;

            await _service.Update(existingData);

            return Ok(existingData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting employee with id: {Id}", id);
            var success = await _service.Delete(id);

            if (!success)
            {
                _logger.LogWarning("Employee with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }

    }
}
*/

using DataServices.Models;
using InterviewApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InterviewApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewController : Controller
    {
        private readonly IInterviewService _service;
        private readonly ILogger<InterviewController> _logger;

        public InterviewController(IInterviewService Service, ILogger<InterviewController> logger)
        {
            _service = Service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Interviews>>> GetAll()
        {
            _logger.LogInformation("Fetching all employeeTechnology");
            var data = await _service.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Interviews>> Get(string id)
        {
            _logger.LogInformation("Fetching employee with id: {Id}", id);
            var data = await _service.Get(id);

            if (data == null)
            {
                _logger.LogWarning("Employee with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add(InterviewsDTO _object)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating Interview");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new Interview");
            try
            {
                var created = await _service.Add(_object);
                return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }

        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] InterviewsDTO _object)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating employee");
                return BadRequest(ModelState);
            }

            if (id != _object.Id)
            {
                _logger.LogWarning("Employee id: {Id} does not match with the id in the request body", id);
                return BadRequest("Employee ID mismatch.");
            }

            _logger.LogInformation("Updating employee with id: {Id}", id);
            try
            {
                await _service.Update(_object);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting employee with id: {Id}", id);
            var success = await _service.Delete(id);

            if (!success)
            {
                _logger.LogWarning("Employee with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }

    }
}