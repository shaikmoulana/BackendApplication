using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.EntityFrameworkCore;

namespace POCAPI.Services
{
    public class POCTeamService : IPOCTeamService
    {
        private readonly IRepository<POCTeam> _repository;
        private readonly DataBaseContext _context;

        public POCTeamService(IRepository<POCTeam> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<POCTeamDTO>> GetAll()
        {
            var pocTeams = await _context.TblPOCTeam
                .Include(e => e.Employee)
                .Include(e => e.POC)
                .ToListAsync();

            var pocTeamDtos = new List<POCTeamDTO>();

            foreach (var poc in pocTeams)
            {
                pocTeamDtos.Add(new POCTeamDTO
                {
                    Id = poc.Id,
                    POC = poc.POC?.Title,
                    Employee = poc.Employee?.Name,
                    IsActive = poc.IsActive,
                    CreatedBy = poc.CreatedBy,
                    CreatedDate = poc.CreatedDate,
                    UpdatedBy = poc.UpdatedBy,
                    UpdatedDate = poc.UpdatedDate
                });
            }
            return pocTeamDtos;
        }

        public async Task<POCTeamDTO> Get(string id)
        {
            var poc = await _context.TblPOCTeam
                .Include(e => e.Employee)
                .Include(e => e.POC)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (poc == null)
                return null;

            return new POCTeamDTO
            {
                Id = poc.Id,
                POC = poc.POC?.Title,
                Employee = poc.Employee?.Name,
                IsActive = poc.IsActive,
                CreatedBy = poc.CreatedBy,
                CreatedDate = poc.CreatedDate,
                UpdatedBy = poc.UpdatedBy,
                UpdatedDate = poc.UpdatedDate
            };
        }

        public async Task<POCTeamDTO> Add(POCTeamDTO _object)
        {
            var employee = await _context.TblEmployee
                .FirstOrDefaultAsync(d => d.Name == _object.Employee);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            var poc = await _context.TblPOC
                .FirstOrDefaultAsync(d => d.Title == _object.POC);

            if (poc == null)
                throw new KeyNotFoundException("POC not found");

            var pocTeam = new POCTeam
            {
                POCId = poc.Id,
                EmployeeId = employee.Id,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblPOCTeam.Add(pocTeam);
            await _context.SaveChangesAsync();

            _object.Id = pocTeam.Id;
            return _object;
        }

        public async Task<POCTeamDTO> Update(POCTeamDTO _object)
        {
            var pocTeam = await _context.TblPOCTeam.FindAsync(_object.Id);

            if (pocTeam == null)
                throw new KeyNotFoundException("PocTeam not found");

            var employee = await _context.TblEmployee
                .FirstOrDefaultAsync(d => d.Name == _object.Employee);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            var poc = await _context.TblPOC
                .FirstOrDefaultAsync(d => d.Title == _object.POC);

            if (poc == null)
                throw new KeyNotFoundException("POC not found");

            pocTeam.POCId = poc?.Id;
            pocTeam.EmployeeId = employee?.Id;
            pocTeam.IsActive = _object.IsActive;
            pocTeam.CreatedBy = _object.CreatedBy;
            pocTeam.CreatedDate = _object.CreatedDate;
            pocTeam.UpdatedBy = _object.UpdatedBy;
            pocTeam.UpdatedDate = _object.UpdatedDate;

            _context.Entry(poc).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            // Check if the Blogs exists
            var existingData = await _repository.Get(id);
            if (existingData == null)
            {
                throw new ArgumentException($"Blogs with ID {id} not found.");
            }
            existingData.IsActive = false; // Soft delete
            await _repository.Update(existingData); // Save changes
            return true;
        }
    }
}
