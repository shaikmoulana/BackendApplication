using DataServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClientApi.Services;
using ClientServices.Services;

namespace ClientApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientContactController : ControllerBase
    {
        private readonly IClientContactService _ClientContactService;
        private readonly ILogger<ClientContactController> _logger;

        public ClientContactController(IClientContactService clientContactService, ILogger<ClientContactController> logger)
        {
            _ClientContactService = clientContactService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientContact>>> GetAll()
        {
            _logger.LogInformation("Fetching all");
            var data = await _ClientContactService.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientContact>> Get(string id)
        {
            _logger.LogInformation("Fetching with id: {Id}", id);
            var data = await _ClientContactService.Get(id);

            if (data == null)
            {
                _logger.LogWarning("with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<ClientContact>> Add([FromBody] ClientContactDTO _object)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating sow");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new sow");
            var data = new ClientContact
            {
                ClientId = _object.ClientId,
                ContactValue = _object.ContactValue,
                ContactType = _object.ContactType,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            var created = await _ClientContactService.Add(data);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ClientContactDTO _object)
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

            var existingData = await _ClientContactService.Get(id);
            if (existingData == null)
            {
                _logger.LogWarning("with id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updating  with id: {Id}", id);

            // Update properties from DTO
            existingData.ClientId = _object.ClientId;
            existingData.ContactValue = _object.ContactValue;
            existingData.ContactType = _object.ContactType;
            existingData.IsActive = _object.IsActive;
            existingData.UpdatedBy = _object.UpdatedBy;
            existingData.UpdatedDate = _object.UpdatedDate;

            await _ClientContactService.Update(existingData);

            return Ok(existingData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting with id: {Id}", id);
            var success = await _ClientContactService.Delete(id);

            if (!success)
            {
                _logger.LogWarning("with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}

