/*using DataServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOWApi.Services;

namespace SOWAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SOWRequirementController : ControllerBase
    {
        private readonly ISOWRequirementService _sowRequirementService;
        private readonly ILogger<SOWRequirementController> _logger;

        public SOWRequirementController(ISOWRequirementService sowRequirementService, ILogger<SOWRequirementController> logger)
        {
            _sowRequirementService = sowRequirementService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SOWRequirement>>> GetAll()
        {
            _logger.LogInformation("Fetching all SOW");
            var sowRequirement = await _sowRequirementService.GetAll();
            return Ok(sowRequirement);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SOWRequirement>> Get(string id)
        {
            _logger.LogInformation("Fetching sow with id: {Id}", id);
            var sowRequirement = await _sowRequirementService.Get(id);

            if (sowRequirement == null)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(sowRequirement);
        }


        [HttpPost]
        public async Task<ActionResult<SOWRequirement>> Add([FromBody] SOWRequirementDTO swoRequirementDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating sow");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new sow");
            var sowRequirement = new SOWRequirement
            {
                SOW = swoRequirementDto.SOW,
                Designation=swoRequirementDto.Designation,
                Technologies=swoRequirementDto.Technologies,
                TeamSize=swoRequirementDto.TeamSize,
                IsActive = swoRequirementDto.IsActive,
                CreatedBy = swoRequirementDto.CreatedBy,
                CreatedDate = swoRequirementDto.CreatedDate,
                UpdatedBy = swoRequirementDto.UpdatedBy,
                UpdatedDate = swoRequirementDto.UpdatedDate
            };

            var created = await _sowRequirementService.Add(sowRequirement);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SOWRequirementDTO swoRequirementDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating sow");
                return BadRequest(ModelState);
            }

            if (id != swoRequirementDto.Id)
            {
                _logger.LogWarning("sow id: {Id} does not match with the id in the request body", id);
                return BadRequest("sow ID mismatch.");
            }

            var existingsow = await _sowRequirementService.Get(id);
            if (existingsow == null)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updating sow with id: {Id}", id);

            // Update properties from DTO
            existingsow.SOW = swoRequirementDto.SOW;
            existingsow.Designation = swoRequirementDto.Designation;
            existingsow.Technologies = swoRequirementDto.Technologies;
            existingsow.TeamSize = swoRequirementDto.TeamSize;
            existingsow.IsActive = swoRequirementDto.IsActive;
            existingsow.UpdatedBy = swoRequirementDto.UpdatedBy;
            existingsow.UpdatedDate = swoRequirementDto.UpdatedDate;

            await _sowRequirementService.Update(existingsow);

            return Ok(existingsow);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting sow with id: {Id}", id);
            var success = await _sowRequirementService.Delete(id);

            if (!success)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}
*/


using DataServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SOWApi.Services;

namespace SOWAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SOWRequirementController : ControllerBase
    {
        private readonly ISOWRequirementService _Service;
        private readonly ILogger<SOWRequirementController> _logger;

        public SOWRequirementController(ISOWRequirementService Service, ILogger<SOWRequirementController> logger)
        {
            _Service = Service;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SOWRequirementDTO>>> GetAll()
        {
            _logger.LogInformation("Fetching all SOW");
            var sowRequirement = await _Service.GetAll();
            return Ok(sowRequirement);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SOWRequirementDTO>> Get(string id)
        {
            _logger.LogInformation("Fetching sow with id: {Id}", id);
            var sowRequirement = await _Service.Get(id);

            if (sowRequirement == null)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(sowRequirement);
        }


        [HttpPost]
        public async Task<ActionResult<SOWRequirementDTO>> Add([FromBody] SOWRequirementDTO _object)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating sow");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new sow");
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
        public async Task<IActionResult> Update(string id, [FromBody] SOWRequirementDTO _object)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating sow");
                return BadRequest(ModelState);
            }

            if (id != _object.Id)
            {
                _logger.LogWarning("sow id: {Id} does not match with the id in the request body", id);
                return BadRequest("sow ID mismatch.");
            }

            _logger.LogInformation("Updating sow with id: {Id}", id);

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
            _logger.LogInformation("Deleting sow with id: {Id}", id);
            var success = await _Service.Delete(id);

            if (!success)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}