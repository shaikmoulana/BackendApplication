﻿using DataServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechnologyApi.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TechnologyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TechnologyController : ControllerBase
    {
        private readonly ITechnologyService _technologyService;
        private readonly ILogger<TechnologyController> _logger;

        public TechnologyController(ITechnologyService technologyService, ILogger<TechnologyController> logger)
        {
            _technologyService = technologyService;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<TechnologyDTO>>> GetTechnologies()
        {
            _logger.LogInformation("Fetching all technologies");
            var technologies = await _technologyService.GetAll();
            if (User.IsInRole("Admin"))
            {
                return Ok(technologies); // Admin can see all data
            }
            else
            {
                return Ok(technologies.Where(d => d.IsActive)); // Non-admins see only active data
            }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TechnologyDTO>> GetTechnology(string id)
        {
            _logger.LogInformation("Fetching technology with id: {Id}", id);
            var technology = await _technologyService.Get(id);

            if (technology == null)
            {
                _logger.LogWarning("Technology with id: {Id} not found", id);
                return NotFound();
            }

            // Check if the logged-in user has the "Admin" role
            if (User.IsInRole("Admin"))
            {
                return Ok(technology); // Admin can see both active and inactive 
            }
            else if (technology.IsActive)
            {
                return Ok(technology); // Non-admins can only see active data
            }
            else
            {
                _logger.LogWarning("Technology with id: {Id} is inactive and user does not have admin privileges", id);
                return Forbid(); // Return forbidden if non-admin tries to access an inactive 
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TechnologyDTO>> Create([FromBody] TechnologyDTO techDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating technology");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new technology");

            try
            {
                var createdTechnology = await _technologyService.Add(techDto);
                return CreatedAtAction(nameof(GetTechnology), new { id = createdTechnology.Id }, createdTechnology);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateTechnology(string id, [FromBody] TechnologyDTO techDto)
        {
            if (id != techDto.Id)
            {
                _logger.LogWarning("Technology id mismatch");
                return BadRequest("Technology ID mismatch");
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating technology");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Updating technology with id: {Id}", id);

            try
            {
                await _technologyService.Update(techDto);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex.Message);
                return NotFound(ex.Message);
            }

            return NoContent();
        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteTechnology(string id)
        {
            _logger.LogInformation("Deleting technology with id: {Id}", id);

            var result = await _technologyService.Delete(id);

            if (!result)
            {
                _logger.LogWarning("Technology with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}