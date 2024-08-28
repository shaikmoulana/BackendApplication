using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ProjectApi.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _repository;
        private readonly DataBaseContext _context;

        public ProjectService(IRepository<Project> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<ProjectDTO>> GetAll()
        {
            var projects = await _context.TblProject
                .Include(c => c.Client)
                .Include(t => t.TechnicalProjectManagerId)
                .Include(s => s.SalesContactId)
                .Include(p => p.PMOId)
                .ToListAsync();

            var projectDto = new List<ProjectDTO>();
            foreach (var project in projects)
            {
                projectDto.Add(new ProjectDTO
                {
                    Id = project.Id,
                    Client = project.Client?.Name,
                    ProjectName = project.ProjectName,
                    TechnicalProjectManager = project.TechnicalProjectManagerId?.Name,
                    SalesContact = project.SalesContactId?.Name,
                    PMO = project.PMOId?.Name,
                    SOWSubmittedDate = project.SOWSubmittedDate,
                    SOWSignedDate = project.SOWSignedDate,
                    SOWValidTill = project.SOWValidTill,
                    SOWLastExtendedDate = project.SOWLastExtendedDate,
                    IsActive = project.IsActive,
                    CreatedBy = project.CreatedBy,
                    CreatedDate = project.CreatedDate,
                    UpdatedBy = project.UpdatedBy,
                    UpdatedDate = project.UpdatedDate
                });
            }
            return projectDto;

        }

        public async Task<ProjectDTO> Get(string id)
        {
            var project = await _context.TblProject
                .Include(c => c.Client)
                .Include(t => t.TechnicalProjectManagerId)
                .Include(s => s.SalesContactId)
                .Include(p => p.PMOId)
                .FirstOrDefaultAsync(t => t.Id == id);
            if (project == null) return null;

            return new ProjectDTO
            {
                Id = project.Id,
                Client = project.Client?.Name,
                ProjectName = project.ProjectName,
                TechnicalProjectManager = project.TechnicalProjectManagerId?.Name,
                SalesContact = project.SalesContactId?.Name,
                PMO = project.PMOId?.Name,
                SOWSubmittedDate = project.SOWSubmittedDate,
                SOWSignedDate = project.SOWSignedDate,
                SOWValidTill = project.SOWValidTill,
                SOWLastExtendedDate = project.SOWLastExtendedDate,
                IsActive = project.IsActive,
                CreatedBy = project.CreatedBy,
                CreatedDate = project.CreatedDate,
                UpdatedBy = project.UpdatedBy,
                UpdatedDate = project.UpdatedDate
            };
        }

        public async Task<ProjectDTO> Add(ProjectDTO _object)
        {
            var client = await _context.TblClient
               .FirstOrDefaultAsync(d => d.Name == _object.Client);

            if (client == null)
                throw new KeyNotFoundException("Client not found");

            var technicalProjectManager = await _context.TblEmployee
               .FirstOrDefaultAsync(d => d.Name == _object.TechnicalProjectManager);

            if (technicalProjectManager == null)
                throw new KeyNotFoundException("TechnicalProjectManagerId not found");

            var salesContact = await _context.TblEmployee
               .FirstOrDefaultAsync(d => d.Name == _object.SalesContact);

            if (salesContact == null)
                throw new KeyNotFoundException("SalesContact not found");

            var pmo = await _context.TblEmployee
               .FirstOrDefaultAsync(d => d.Name == _object.PMO);

            if (pmo == null)
                throw new KeyNotFoundException("PMO not found");

            var project = new Project
            {
                ClientId = client.Id,
                ProjectName = _object.ProjectName,
                TechnicalProjectManager = technicalProjectManager.Id,
                SalesContact = salesContact.Id,
                PMO = pmo.Id,
                SOWSubmittedDate = _object.SOWSubmittedDate,
                SOWSignedDate = _object.SOWSignedDate,
                SOWValidTill = _object.SOWValidTill,
                SOWLastExtendedDate = _object.SOWLastExtendedDate,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblProject.Add(project);
            await _context.SaveChangesAsync();

            _object.Id = project.Id;
            return _object;
        }

        public async Task<ProjectDTO> Update(ProjectDTO _object)
        {
            var project = await _context.TblProject.FindAsync(_object.Id);

            if (project == null)
                throw new KeyNotFoundException("Project not found");

            var client = await _context.TblClient
              .FirstOrDefaultAsync(d => d.Name == _object.Client);

            if (client == null)
                throw new KeyNotFoundException("Client not found");

            var technicalProjectManager = await _context.TblEmployee
                .FirstOrDefaultAsync(d => d.Name == _object.TechnicalProjectManager);

            if (technicalProjectManager == null)
                throw new KeyNotFoundException("TechnicalProjectManagerId not found");

            var salesContact = await _context.TblEmployee
               .FirstOrDefaultAsync(d => d.Name == _object.SalesContact);

            if (salesContact == null)
                throw new KeyNotFoundException("SalesContact not found");

            var pmo = await _context.TblEmployee
               .FirstOrDefaultAsync(d => d.Name == _object.PMO);

            if (pmo == null)
                throw new KeyNotFoundException("PMO not found");

            project.ClientId = client.Id;
            project.ProjectName = _object.ProjectName;
            project.TechnicalProjectManager = technicalProjectManager.Id;
            project.SalesContact = salesContact.Id;
            project.PMO = pmo.Id;
            project.SOWSubmittedDate = _object.SOWSubmittedDate;
            project.SOWSignedDate = _object.SOWSignedDate;
            project.SOWValidTill = _object.SOWValidTill;
            project.SOWLastExtendedDate = _object.SOWLastExtendedDate;
            project.IsActive = _object.IsActive;
            project.CreatedBy = _object.CreatedBy;
            project.CreatedDate = _object.CreatedDate;
            project.UpdatedBy = _object.UpdatedBy;
            project.UpdatedDate = _object.UpdatedDate;

            _context.Entry(project).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            // Check if the technology exists
            var existingData = await _repository.Get(id);
            if (existingData == null)
            {
                throw new ArgumentException($"with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}