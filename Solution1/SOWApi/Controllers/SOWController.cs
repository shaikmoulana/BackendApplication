using DataServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOWApi.Services;

namespace SOWApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SOWController : ControllerBase
    {
        private readonly ISOWService _sowService;
        private readonly ILogger<SOWController> _logger;

        public SOWController(ISOWService sowService, ILogger<SOWController> logger)
        {
            _sowService = sowService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SOW>>> GetAll()
        {
            _logger.LogInformation("Fetching all SOW");
            var sow = await _sowService.GetAll();
            return Ok(sow);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SOW>> Get(string id)
        {
            _logger.LogInformation("Fetching sow with id: {Id}", id);
            var sow = await _sowService.Get(id);

            if (sow == null)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(sow);
        }


        [HttpPost]
        public async Task<ActionResult<SOW>> Add([FromBody] SOWDTO sowDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating sow");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new sow");
            var sow = new SOW
            {
                Client = sowDTO.Client,
                Project = sowDTO.Project,
                PreparedDate = sowDTO.PreparedDate,
                SubmittedDate = sowDTO.SubmittedDate,
                Status = sowDTO.Status,
                Comments = sowDTO.Comments,
                IsActive = sowDTO.IsActive,
                CreatedBy = sowDTO.CreatedBy,
                CreatedDate = sowDTO.CreatedDate,
                UpdatedBy = sowDTO.UpdatedBy,
                UpdatedDate = sowDTO.UpdatedDate
            };

            var created= await _sowService.Add(sow);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SOWDTO sowDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating sow");
                return BadRequest(ModelState);
            }

            if (id != sowDto.Id)
            {
                _logger.LogWarning("sow id: {Id} does not match with the id in the request body", id);
                return BadRequest("sow ID mismatch.");
            }

            var existingsow= await _sowService.Get(id);
            if (existingsow == null)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updating sow with id: {Id}", id);

            // Update properties from DTO
            existingsow.Client=sowDto.Client;
            existingsow.Project=sowDto.Project;
            existingsow.PreparedDate=sowDto.PreparedDate;
            existingsow.SubmittedDate=sowDto.SubmittedDate;
            existingsow.Status = sowDto.Status;
            existingsow.Comments = sowDto.Comments;
            existingsow.IsActive = sowDto.IsActive;
            existingsow.UpdatedBy = sowDto.UpdatedBy;
            existingsow.UpdatedDate = sowDto.UpdatedDate;

            await _sowService.Update(existingsow);

            return Ok(existingsow);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting sow with id: {Id}", id);
            var success = await _sowService.Delete(id);

            if (!success)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}
