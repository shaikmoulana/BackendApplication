﻿using DataServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClientApi.Services;
using ClientServices.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead, Team Member")]
        public async Task<ActionResult<IEnumerable<ClientContactDTO>>> GetAll()
        {
            _logger.LogInformation("Fetching all");
            var data = await _ClientContactService.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead, Team Member")]
        public async Task<ActionResult<ClientContactDTO>> Get(string id)
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
        [Authorize(Roles = "Admin, Director, Project Manager")]
        public async Task<ActionResult<ClientContactDTO>> Add([FromBody] ClientContactDTO _object)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating sow");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new sow");

            try
            {
                var created = await _ClientContactService.Add(_object);
                return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }

        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead")]
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

            try
            {
                await _ClientContactService.Update(_object);
            }

            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }

            return NoContent();

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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