using DataServices.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlogsApi.Services;

namespace BlogsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IBlogsService _blogsService;
        private readonly ILogger<BlogsController> _logger;

        public BlogsController(IBlogsService blogsService, ILogger<BlogsController> logger)
        {
            _blogsService = blogsService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Blogs>>> GetAll()
        {
            _logger.LogInformation("Fetching all ");
            var data = await _blogsService.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Blogs>> Get(string id)
        {
            _logger.LogInformation("Fetching with id: {Id}", id);
            var blogs = await _blogsService.Get(id);

            if (blogs == null)
            {
                _logger.LogWarning("with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(blogs);
        }


        [HttpPost]
        public async Task<ActionResult<Blogs>> Add([FromBody] BlogsDTO blogsDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating sow");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new sow");
            var blogs = new Blogs
            {
                Title = blogsDTO.Title,
                Author = blogsDTO.Author,
                Status = blogsDTO.Status,
                TargetDate = blogsDTO.TargetDate,
                CompletedDate = blogsDTO.CompletedDate,
                PublishedDate = blogsDTO.PublishedDate,
                IsActive = blogsDTO.IsActive,
                CreatedBy = blogsDTO.CreatedBy,
                CreatedDate = blogsDTO.CreatedDate,
                UpdatedBy = blogsDTO.UpdatedBy,
                UpdatedDate = blogsDTO.UpdatedDate
            };

            var created = await _blogsService.Add(blogs);
            return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] BlogsDTO blogsDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating sow");
                return BadRequest(ModelState);
            }

            if (id != blogsDTO.Id)
            {
                _logger.LogWarning("sow id: {Id} does not match with the id in the request body", id);
                return BadRequest("sow ID mismatch.");
            }

            var existingData = await _blogsService.Get(id);
            if (existingData  == null)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            _logger.LogInformation("Updating sow with id: {Id}", id);

            // Update properties from DTO
            existingData.Title = blogsDTO.Title;
            existingData.Author = blogsDTO.Author;
            existingData.Status = blogsDTO.Status;
            existingData.TargetDate = blogsDTO.TargetDate;
            existingData.CompletedDate = blogsDTO.CompletedDate;
            existingData.PublishedDate = blogsDTO.PublishedDate;
            existingData.IsActive = blogsDTO.IsActive;
            existingData.UpdatedBy = blogsDTO.UpdatedBy;
            existingData.UpdatedDate = blogsDTO.UpdatedDate;

            await _blogsService.Update(existingData);

            return Ok(existingData);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            _logger.LogInformation("Deleting sow with id: {Id}", id);
            var success = await _blogsService.Delete(id);

            if (!success)
            {
                _logger.LogWarning("sow with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}

