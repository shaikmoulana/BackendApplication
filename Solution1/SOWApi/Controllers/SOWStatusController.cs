using DataServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOWApi.Services;

namespace SOWAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SOWStatusController : ControllerBase
    {
        private readonly ISOWStatusService _sowStatusService;
        private readonly ILogger<SOWStatusController> _logger;

        public SOWStatusController(ISOWStatusService sowStatusService, ILogger<SOWStatusController> logger)
        {
            _sowStatusService = sowStatusService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead, Team Member")]
        public async Task<ActionResult<IEnumerable<SOWStatus>>> GetAll()
        {
            _logger.LogInformation("Fetching all SOWStatus data");
            var sowStatus = await _sowStatusService.GetAll();
            return Ok(sowStatus);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead, Team Member")]
        public async Task<ActionResult<SOWStatus>> Get(string id)
        {
            _logger.LogInformation("Fetching sowStatus with id: {Id}", id);
            var sowStatus = await _sowStatusService.Get(id);

            if (sowStatus == null)
            {
                _logger.LogWarning("sowStatus with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(sowStatus);
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Director, Project Manager")]
        public async Task<ActionResult<SOWStatus>> Add([FromBody] SOWStatusDTO swoStatusDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating sowStatus");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new sowtatus");
            var sowStatus = new SOWStatus
            {
                Status = swoStatusDto.Status,
                IsActive = swoStatusDto.IsActive,
                CreatedBy = swoStatusDto.CreatedBy,
                CreatedDate = swoStatusDto.CreatedDate,
                UpdatedBy = swoStatusDto.UpdatedBy,
                UpdatedDate = swoStatusDto.UpdatedDate
            };

            var created = await _sowStatusService.Add(sowStatus);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead")]
        public async Task<IActionResult> Update(string id, [FromBody] SOWStatusDTO swoStatusDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating sowStatus");
                return BadRequest(ModelState);
            }

            if (id != swoStatusDto.Id)
            {
                _logger.LogWarning("sowStatus id: {Id} does not match with the id in the request body", id);
                return BadRequest("sowStatus ID mismatch.");
            }

            var existingsow = await _sowStatusService.Get(id);
            if (existingsow == null)
            {
                _logger.LogWarning("sowStatus with id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updating sowStatus with id: {Id}", id);

            // Update properties from DTO
            existingsow.Status = swoStatusDto.Status;
            existingsow.IsActive = swoStatusDto.IsActive;
            existingsow.UpdatedBy = swoStatusDto.UpdatedBy;
            existingsow.UpdatedDate = swoStatusDto.UpdatedDate;

            await _sowStatusService.Update(existingsow);

            return Ok(existingsow);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting sowStatus with id: {Id}", id);
            var success = await _sowStatusService.Delete(id);

            if (!success)
            {
                _logger.LogWarning("sowStatus with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}
