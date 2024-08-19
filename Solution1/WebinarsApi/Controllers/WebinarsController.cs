using DataServices.Models;
using WebinarsApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebinarsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebinarsController : ControllerBase
    {
        private readonly IWebinarService _Service;
        private readonly ILogger<WebinarsController> _logger;

        public WebinarsController(IWebinarService service, ILogger<WebinarsController> logger)
        {
            _Service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Webinars>>> GetAll()
        {
            _logger.LogInformation("Fetching all");
            var data = await _Service.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Webinars>> Get(string id)
        {
            _logger.LogInformation("Fetching with id: {Id}", id);
            var data = await _Service.Get(id);

            if (data == null)
            {
                _logger.LogWarning("with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<Webinars>> Add([FromBody] WebinarsDTO _object)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new");
            var data = new Webinars
            {
                Title = _object.Title,
                Speaker = _object.Speaker,
                Status = _object.Status,
                WebinarDate = _object.WebinarDate,
                NumberOfAudience = _object.NumberOfAudience,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            var created = await _Service.Add(data);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] WebinarsDTO _object)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating ");
                return BadRequest(ModelState);
            }

            if (id != _object.Id)
            {
                _logger.LogWarning("id: {Id} does not match with the id in the request body", id);
                return BadRequest("ID mismatch.");
            }

            var existingData = await _Service.Get(id);
            if (existingData == null)
            {
                _logger.LogWarning("with id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updating  with id: {Id}", id);

            // Update properties from DTO
            existingData.Title = _object.Title;
            existingData.Speaker = _object.Speaker;
            existingData.Status = _object.Status;
            existingData.WebinarDate = _object.WebinarDate;
            existingData.NumberOfAudience = _object.NumberOfAudience;
            existingData.IsActive = _object.IsActive;
            existingData.UpdatedBy = _object.UpdatedBy;
            existingData.UpdatedDate = _object.UpdatedDate;

            await _Service.Update(existingData);

            return Ok(existingData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting with id: {Id}", id);
            var success = await _Service.Delete(id);

            if (!success)
            {
                _logger.LogWarning("with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}
