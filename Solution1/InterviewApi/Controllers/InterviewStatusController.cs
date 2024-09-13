﻿using DataServices.Models;
using InterviewApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class InterviewStatusController : ControllerBase
{
    private readonly IInterviewStatusService _Service;
    private readonly ILogger<InterviewStatusController> _logger;


    public InterviewStatusController(IInterviewStatusService Service, ILogger<InterviewStatusController> logger)
    {
        _Service = Service;
        _logger = logger;
    }

    [HttpGet]
    [Authorize(Roles = "Admin, Director, Project Manager, Team Lead, Team Member")]
    public async Task<ActionResult<IEnumerable<InterviewStatusDTO>>> GetAll()
    {
        _logger.LogInformation("Fetching all");
        var interviewStatus = await _Service.GetAll();
        return Ok(interviewStatus);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin, Director, Project Manager, Team Lead, Team Member")]
    public async Task<ActionResult<InterviewStatusDTO>> Get(string id)
    {
        _logger.LogInformation("Fetching with id: {id}", id);
        var interviewStatus = await _Service.Get(id);
        return Ok(interviewStatus);
    }



    [HttpPost]
    [Authorize(Roles = "Admin, Director, Project Manager")]
    public async Task<IActionResult> Add([FromBody] InterviewStatusDTO _object)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for creating");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Creating a new interview status");

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
    public async Task<IActionResult> Update(string id, [FromBody] InterviewStatusDTO _object)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for updating employee");
            return BadRequest(ModelState);
        }

        if (id != _object.Id)
        {
            _logger.LogWarning("Employee id: {Id} does not match with the id in the request body", id);
            return BadRequest("Employee ID mismatch.");
        }

        _logger.LogInformation("Updating employee with id: {Id}", id);
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
        _logger.LogInformation("Deleting interview with id: {Id}", id);
        var success = await _Service.Delete(id);

        if (!success)
        {
            _logger.LogWarning("InterviewStatus with id: {Id} not found", id);
            return NotFound();
        }

        return NoContent();
    }


}