using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ProjectApi.Services
{
    public class ProjectTechnologyService : IProjectTechnologyService
    {
        private readonly IRepository<ProjectTechnology> _repository;
        private readonly DataBaseContext _context;

        public ProjectTechnologyService(IRepository<ProjectTechnology> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<ProjectTechnologyDTO>> GetAll()
        {
            var projectTechnologies = await _context.TblProjectTechnology
                .Include(p => p.Project)
                .Include(p => p.Technologies)
                .ToListAsync();

            var projectTechnology = new List<ProjectTechnologyDTO>();

            foreach (var item in projectTechnologies)
            {
                projectTechnology.Add(new ProjectTechnologyDTO
                {
                    Id = item.Id,
                    Project = item.Project?.ProjectName,
                    Technology = item.Technologies?.Name,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate
                });
            }
            return projectTechnology;

        }

        public async Task<ProjectTechnologyDTO> Get(string id)
        {
            var projectTechnology = await _context.TblProjectTechnology
               .Include(p => p.Project)
               .Include(p => p.Technology)
               .FirstOrDefaultAsync(t => t.Id == id);

            if (projectTechnology == null) return null;


            return new ProjectTechnologyDTO
            {
                Id = projectTechnology.Id,
                Project = projectTechnology.Project?.ProjectName,
                Technology = projectTechnology.Technologies?.Name,
                IsActive = projectTechnology.IsActive,
                CreatedBy = projectTechnology.CreatedBy,
                CreatedDate = projectTechnology.CreatedDate,
                UpdatedBy = projectTechnology.UpdatedBy,
                UpdatedDate = projectTechnology.UpdatedDate

            };
        }

        public async Task<ProjectTechnologyDTO> Add(ProjectTechnologyDTO _object)
        {
            var project = await _context.TblProject
               .FirstOrDefaultAsync(d => d.ProjectName == _object.Project);

            if (project == null)
                throw new KeyNotFoundException("Project not found");

            var technology = await _context.TblTechnology
               .FirstOrDefaultAsync(d => d.Name == _object.Technology);

            if (technology == null)
                throw new KeyNotFoundException("Technology not found");

            var projectTechnology = new ProjectTechnology
            {
                ProjectId = project?.Id,
                TechnologyId = technology?.Id,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblProjectTechnology.Add(projectTechnology);
            await _context.SaveChangesAsync();

            _object.Id = projectTechnology.Id;
            return _object;
        }

        public async Task<ProjectTechnologyDTO> Update(ProjectTechnologyDTO _object)
        {
            var projectTechnology = await _context.TblProjectTechnology.FindAsync(_object.Id);

            if (projectTechnology == null)
                throw new KeyNotFoundException("ProjectTechnology not found");

            var project = await _context.TblProject
              .FirstOrDefaultAsync(d => d.ProjectName == _object.Project);

            if (project == null)
                throw new KeyNotFoundException("Project not found");

            var technology = await _context.TblTechnology
                .FirstOrDefaultAsync(d => d.Name == _object.Technology);

            if (technology == null)
                throw new KeyNotFoundException("technology not found");


            projectTechnology.ProjectId = project?.Id;
            projectTechnology.TechnologyId = technology?.Id;
            projectTechnology.IsActive = _object.IsActive;
            projectTechnology.CreatedBy = _object.CreatedBy;
            projectTechnology.CreatedDate = _object.CreatedDate;
            projectTechnology.UpdatedBy = _object.UpdatedBy;
            projectTechnology.UpdatedDate = _object.UpdatedDate;

            _context.Entry(projectTechnology).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _object;

        }

        public async Task<bool> Delete(string id)
        {
            var existingData = await _repository.Get(id);
            if (existingData == null)
            {
                throw new ArgumentException($"with ID {id} not found.");
            }
            existingData.IsActive = false; // Soft delete
            await _repository.Update(existingData); // Save changes
            return true;
        }
    }
}