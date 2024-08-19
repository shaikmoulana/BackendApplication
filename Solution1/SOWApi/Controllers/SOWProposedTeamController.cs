using DataServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOWApi.Services;

namespace SOWApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SOWProposedTeamController : ControllerBase
    {
        private readonly ISOWProposedTeamService _sowProposedTeamService;
        private readonly ILogger<SOWProposedTeamController> _logger;

        public SOWProposedTeamController(ISOWProposedTeamService sowProposedTeamService, ILogger<SOWProposedTeamController> logger)
        {
            _sowProposedTeamService = sowProposedTeamService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SOWProposedTeam>>> GetAll()
        {
            _logger.LogInformation("Fetching all SOW");
            var sowProposedTeam = await _sowProposedTeamService.GetAll();
            return Ok(sowProposedTeam);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SOWProposedTeam>> Get(string id)
        {
            _logger.LogInformation("Fetching sow with id: {Id}", id);
            var sowProposedTeam = await _sowProposedTeamService.Get(id);

            if (sowProposedTeam == null)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(sowProposedTeam);
        }


        [HttpPost]
        public async Task<ActionResult<SOWProposedTeam>> Add([FromBody] SOWProposedTeamDTO swoProposedTeamDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating sow");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new sow");
            var sowProposedTeam = new SOWProposedTeam
            {
                SOWRequirement = swoProposedTeamDto.SOWRequirement,
                Employee = swoProposedTeamDto.Employee,
                IsActive = swoProposedTeamDto.IsActive,
                CreatedBy = swoProposedTeamDto.CreatedBy,
                CreatedDate = swoProposedTeamDto.CreatedDate,
                UpdatedBy = swoProposedTeamDto.UpdatedBy,
                UpdatedDate = swoProposedTeamDto.UpdatedDate
            };

            var created = await _sowProposedTeamService.Add(sowProposedTeam);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SOWProposedTeamDTO sowProposedTeamDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating sow");
                return BadRequest(ModelState);
            }

            if (id != sowProposedTeamDto.Id)
            {
                _logger.LogWarning("sow id: {Id} does not match with the id in the request body", id);
                return BadRequest("sow ID mismatch.");
            }

            var existingsow = await _sowProposedTeamService.Get(id);
            if (existingsow == null)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updating sow with id: {Id}", id);

            // Update properties from DTO
            existingsow.SOWRequirement = sowProposedTeamDto.SOWRequirement;
            existingsow.Employee= sowProposedTeamDto.Employee;
            existingsow.IsActive = sowProposedTeamDto.IsActive;
            existingsow.UpdatedBy = sowProposedTeamDto.UpdatedBy;
            existingsow.UpdatedDate = sowProposedTeamDto.UpdatedDate;

            await _sowProposedTeamService.Update(existingsow);

            return Ok(existingsow);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting sow with id: {Id}", id);
            var success = await _sowProposedTeamService.Delete(id);

            if (!success)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}
