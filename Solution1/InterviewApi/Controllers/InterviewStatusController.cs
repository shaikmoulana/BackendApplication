/*using DataServices.Models;
using InterviewApi.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class InterviewStatusController : ControllerBase
{
    private readonly IInterviewStatusService _interviewStatusService;
    private readonly ILogger<InterviewStatusController> _logger;


    public InterviewStatusController(IInterviewStatusService interviewStatusService, ILogger<InterviewStatusController> logger)
    {
        _interviewStatusService = interviewStatusService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InterviewStatus>>> GetAll()
    {
        _logger.LogInformation("Fetching all");
        var interviewStatus = await _interviewStatusService.GetAll();
        return Ok(interviewStatus);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InterviewStatus>> Get(string id)
    {
        _logger.LogInformation("Fetching with id: {id}", id);
        var interviewStatus = await _interviewStatusService.Get(id);
        return NotFound();
    }

   

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] InterviewStatusDTO interviewStatusDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for creating");
            return BadRequest(ModelState);
        }

        _logger.LogInformation("Creating a new interview status");

        var interviewStatus = new InterviewStatus
        {
            Status = interviewStatusDto.Status,
            IsActive = interviewStatusDto.IsActive,
            CreatedBy = interviewStatusDto.CreatedBy,
            CreatedDate = DateTime.Now,
            UpdatedBy = interviewStatusDto.UpdatedBy,
            UpdatedDate = DateTime.Now
        };

        var created = await _interviewStatusService.Create(interviewStatus);
        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }



    [HttpPut("{id}")]
    public async void Update(string id, [FromBody] InterviewStatusDTO interviewStatusDto)
    {
        if (!ModelState.IsValid)
        {
            _logger.LogWarning("Invalid model state for updating interview");
            // Handle the bad request, e.g., by setting a result
            return;
        }

        if (id != interviewStatusDto.Id)
        {
            _logger.LogWarning("InterviewStatus id: {Id} does not match with the id in the request body", id);
            // Handle the ID mismatch, e.g., by setting a result
            return;
        }

        try
        {
            var existingInterviewStatus = await _interviewStatusService.Get(id);
            if (existingInterviewStatus == null)
            {
                _logger.LogWarning("Interview with id: {Id} not found", id);
                // Handle the not found case, e.g., by setting a result
                return;
            }

            _logger.LogInformation("Updating interview with id: {Id}", id);

            // Update properties from DTO
            
            existingInterviewStatus.Status = interviewStatusDto.Status;
            existingInterviewStatus.IsActive = interviewStatusDto.IsActive;
            existingInterviewStatus.CreatedBy = interviewStatusDto.CreatedBy;
            existingInterviewStatus.CreatedDate = DateTime.Now;
            existingInterviewStatus.UpdatedBy = interviewStatusDto.UpdatedBy;
            existingInterviewStatus.UpdatedDate = DateTime.Now;

            await _interviewStatusService.Update(existingInterviewStatus);
            // Do something after update if needed, like setting a result
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating interview with id: {Id}", id);
            // Handle the error, e.g., by setting a result
        }
    }


    [HttpDelete("{id}")]
    public async void Delete(string id)
    {
        _logger.LogInformation("Deleting with id: {Id}", id);
        try
        {
            _interviewStatusService.Delete(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deleting id: {Id}", id);
        }
    }


}
*/


using DataServices.Models;
using InterviewApi.Services;
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
    public async Task<ActionResult<IEnumerable<InterviewStatusDTO>>> GetAll()
    {
        _logger.LogInformation("Fetching all");
        var interviewStatus = await _Service.GetAll();
        return Ok(interviewStatus);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<InterviewStatusDTO>> Get(string id)
    {
        _logger.LogInformation("Fetching with id: {id}", id);
        var interviewStatus = await _Service.Get(id);
        return Ok(interviewStatus);
    }



    [HttpPost]
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