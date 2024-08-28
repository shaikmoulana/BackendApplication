using DataServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClientApi.Services;
using ClientServices.Services;
using System.Diagnostics.Metrics;

namespace ClientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _Service;
        private readonly ILogger<ClientController> _logger;

        public ClientController(IClientService service, ILogger<ClientController> logger)
        {
            _Service = service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientDTO>>> GetAll()
        {
            _logger.LogInformation("Fetching all");
            var data = await _Service.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> Get(string id)
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
        public async Task<ActionResult<ClientDTO>> Create([FromBody] ClientDTO _object)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating sow");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new Client");
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
        public async Task<IActionResult> Update(string id, [FromBody] ClientDTO _object)
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
                return NotFound(ex.Message);
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