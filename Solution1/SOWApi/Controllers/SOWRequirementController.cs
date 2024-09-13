﻿using DataServices.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead, Team Member")]
        public async Task<ActionResult<IEnumerable<SOWRequirementDTO>>> GetAll()
        {
            _logger.LogInformation("Fetching all SOW");
            var sowRequirement = await _Service.GetAll();
            return Ok(sowRequirement);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead, Team Member")]
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
        [Authorize(Roles = "Admin, Director, Project Manager")]
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
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead")]
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
        [Authorize(Roles = "Admin")]
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