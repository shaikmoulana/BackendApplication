/*using DataServices.Models;
using ProjectApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _Service;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(IProjectService service, ILogger<ProjectController> logger)
        {
            _Service = service;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead, Team Member")]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetAll()
        {
            _logger.LogInformation("Fetching all");
            var data = await _Service.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Director, Project Manager, Team Lead, Team Member")]
        public async Task<ActionResult<ProjectDTO>> Get(string id)
        {
            _logger.LogInformation("Fetching with id: {Id}", id);
            var data = await _Service.Get(id);

            if (data == null)
            {
                _logger.LogWarning("with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(data);
        }


        [HttpPost]
        [Authorize(Roles = "Admin, Director, Project Manager")]
        public async Task<ActionResult<ProjectDTO>> Add([FromBody] ProjectDTO _object)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for creating");
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Creating a new");

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
        public async Task<IActionResult> Update(string id, [FromBody] ProjectDTO _object)
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
            _logger.LogInformation("Deleting with id: {Id}", id);
            var success = await _Service.Delete(id);

            if (!success)
            {
                _logger.LogWarning("with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}*/

using DataServices.Models;
using ProjectApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using DataServices.Data;

namespace ProjectApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _Service;
        private readonly ILogger<ProjectController> _logger;
        private readonly DataBaseContext _context;

        public ProjectController(IProjectService service, ILogger<ProjectController> logger, DataBaseContext context)
        {
            _Service = service;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetAll()
        {
            _logger.LogInformation("Fetching all");
            var data = await _Service.GetAll();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDTO>> Get(string id)
        {
            _logger.LogInformation("Fetching with id: {Id}", id);
            var data = await _Service.Get(id);

            if (data == null)
            {
                _logger.LogWarning("with id: {Id} not found", id);
                return NotFound();
            }

            return Ok(data);
        }


        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> Add([FromBody] ProjectDTO projDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var client = await _context.TblClient
                   .FirstOrDefaultAsync(d => d.Name == projDto.Client);

                if (client == null)
                {
                    return BadRequest(new { error = "Invalid client name" });
                }
                var technicalProjectManager = await _context.TblEmployee
                  .FirstOrDefaultAsync(d => d.Name == projDto.TechnicalProjectManager);

                if (technicalProjectManager == null)
                {
                    return BadRequest(new { error = "Invalid technicalProjectManager name" });
                }
                var salesContact = await _context.TblEmployee
                 .FirstOrDefaultAsync(d => d.Name == projDto.SalesContact);

                if (salesContact == null)
                {
                    return BadRequest(new { error = "Invalid salesContact name" });
                }
                var pmo = await _context.TblEmployee
                 .FirstOrDefaultAsync(d => d.Name == projDto.PMO);

                if (pmo == null)
                {
                    return BadRequest(new { error = "Invalid pmo name" });
                }

                var project = new Project
                {
                    ClientId = client.Id,
                    ProjectName = projDto.ProjectName,
                    TechnicalProjectManager = technicalProjectManager.Id,
                    SalesContact = salesContact.Id,
                    PMO = pmo.Id,
                    SOWSubmittedDate = projDto.SOWSubmittedDate,
                    SOWSignedDate = projDto.SOWSignedDate,
                    SOWValidTill = projDto.SOWValidTill,
                    SOWLastExtendedDate = projDto.SOWLastExtendedDate,
                    IsActive = projDto.IsActive,
                    CreatedBy = projDto.CreatedBy,
                    CreatedDate = DateTime.Now,
                    UpdatedBy = projDto.UpdatedBy,
                    UpdatedDate = DateTime.Now,
                };
                await _context.TblProject.AddAsync(project);
                await _context.SaveChangesAsync();

                if (projDto.Technology != null && projDto.Technology.Any())
                {
                    foreach (var technologyId in projDto.Technology)
                    {
                        var projectTechnology = new ProjectTechnology
                        {
                            ProjectId = projDto.Id,
                            TechnologyId = technologyId.ToString(),
                        };

                        await _context.TblProjectTechnology.AddAsync(projectTechnology);
                    }

                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] ProjectDTO projDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for updating ");
                return BadRequest(ModelState);
            }

            if (id != projDto.Id)
            {
                _logger.LogWarning("id: {Id} does not match with the id in the request body", id);
                return BadRequest("ID mismatch.");
            }

            try
            {
                await _Service.Update(projDto);
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
            _logger.LogInformation("Deleting with id: {Id}", id);
            var success = await _Service.Delete(id);

            if (!success)
            {
                _logger.LogWarning("with id: {Id} not found", id);
                return NotFound();
            }

            return NoContent();
        }
    }
}
