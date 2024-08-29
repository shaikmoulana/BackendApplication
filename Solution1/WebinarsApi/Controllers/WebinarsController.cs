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
        public async Task<ActionResult<IEnumerable<WebinarsDTO>>> GetAll()
        {
            _logger.LogInformation("Fetching all");
            var data = await _Service.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WebinarsDTO>> Get(string id)
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
        public async Task<ActionResult<WebinarsDTO>> Add([FromBody] WebinarsDTO _object)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new");

            try
            {
                var created = await _Service.Add(_object);
                return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }

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

            _logger.LogInformation("Updating  with id: {Id}", id);

            try
            {
                await _Service.Update(_object);

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