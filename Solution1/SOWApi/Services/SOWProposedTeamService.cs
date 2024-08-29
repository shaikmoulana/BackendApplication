/*using DataServices.Models;
using DataServices.Repositories;

namespace SOWApi.Services
{
    public class SOWProposedTeamService : ISOWProposedTeamService
    {
        private readonly IRepository<SOWProposedTeam> _repository;

        public SOWProposedTeamService(IRepository<SOWProposedTeam> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SOWProposedTeam>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SOWProposedTeam> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<SOWProposedTeam> Add(SOWProposedTeam sowproposedteam)
        {
            
            *//*sowproposedteam.SOWRequirement = sowproposedteam.SOWRequirement;
            sowproposedteam.Employee = sowproposedteam.Employee;
            sowproposedteam.IsActive = true;
            sowproposedteam.CreatedBy = "SYSTEM";
            sowproposedteam.CreatedDate = DateTime.Now;
            sowproposedteam.UpdatedDate = sowproposedteam.UpdatedDate;
            sowproposedteam.UpdatedBy = sowproposedteam.UpdatedBy;*//*
            return await _repository.Create(sowproposedteam);
        }

        public async Task<SOWProposedTeam> Update(SOWProposedTeam sowproposedteam)
        {
            // Retrieve the existing technology from the database
            *//*var existingsowproposedteam = await _repository.Get(sowproposedteam.Id);
            if (existingsowproposedteam == null)
            {
                throw new ArgumentException($"Technology with ID {sowproposedteam.Id} not found.");
            }

            // Update properties with the new values
            existingsowproposedteam.SOWRequirement = sowproposedteam.SOWRequirement;
            existingsowproposedteam.Employee = sowproposedteam.Employee;
            existingsowproposedteam.UpdatedDate = DateTime.Now;
            existingsowproposedteam.UpdatedBy = sowproposedteam.UpdatedBy;
            existingsowproposedteam.UpdatedDate = sowproposedteam.UpdatedDate;
            // Call repository to update the technology
            return await _repository.Update(existingsowproposedteam);*//*
            return await _repository.Update(sowproposedteam);
        }

        public async Task<bool> Delete(string id)
        {
            // Check if the technology exists
            var existingsowproposedteam = await _repository.Get(id);
            if (existingsowproposedteam == null)
            {
                throw new ArgumentException($"Technology with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}
*/
using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SOWApi.Services
{
    public class SOWProposedTeamService : ISOWProposedTeamService
    {
        private readonly IRepository<SOWProposedTeam> _repository;
        private readonly DataBaseContext _context;

        public SOWProposedTeamService(IRepository<SOWProposedTeam> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<SOWProposedTeamDTO>> GetAll()
        {
            var sowProposedTeams = await _context.TblSOWProposedTeam
               .Include(c => c.SOWRequirements)
               .Include(t => t.Employees)
               .ToListAsync();

            var sowproposedTeamDto = new List<SOWProposedTeamDTO>();
            foreach (var item in sowProposedTeams)
            {
                sowproposedTeamDto.Add(new SOWProposedTeamDTO
                {
                    Id = item.Id,
                    SOWRequirement = item.SOWRequirements?.Technologies,
                    Employee = item.Employees?.Name,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate
                });
            }
            return sowproposedTeamDto;
        }

        public async Task<SOWProposedTeamDTO> Get(string id)
        {
            var sowProposedTeams = await _context.TblSOWProposedTeam
               .Include(c => c.SOWRequirements)
               .Include(t => t.Employees)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (sowProposedTeams == null) return null;

            return new SOWProposedTeamDTO
            {
                Id = sowProposedTeams.Id,
                SOWRequirement = sowProposedTeams.SOWRequirements?.Technologies,
                Employee = sowProposedTeams.Employees?.Name,
                IsActive = sowProposedTeams.IsActive,
                CreatedBy = sowProposedTeams.CreatedBy,
                CreatedDate = sowProposedTeams.CreatedDate,
                UpdatedBy = sowProposedTeams.UpdatedBy,
                UpdatedDate = sowProposedTeams.UpdatedDate
            };
        }

        public async Task<SOWProposedTeamDTO> Add(SOWProposedTeamDTO _object)
        {

            var sowRequirement = await _context.TblSOWRequirement
               .FirstOrDefaultAsync(d => d.Technologies == _object.SOWRequirement);

            if (sowRequirement == null)
                throw new KeyNotFoundException("SowRequirement not found");

            var employee = await _context.TblEmployee
               .FirstOrDefaultAsync(d => d.Name == _object.Employee);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found");


            var sowProposedTeam = new SOWProposedTeam
            {
                SOWRequirementId = sowRequirement?.Id,
                EmployeeId = employee?.Id,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblSOWProposedTeam.Add(sowProposedTeam);
            await _context.SaveChangesAsync();

            _object.Id = sowProposedTeam.Id;
            return _object;
        }

        public async Task<SOWProposedTeamDTO> Update(SOWProposedTeamDTO _object)
        {
            var sowProposedTeam = await _context.TblSOWProposedTeam.FindAsync(_object.Id);

            if (sowProposedTeam == null)
                throw new KeyNotFoundException("sowProposedTeam not found");

            var sowRequirement = await _context.TblSOWRequirement
               .FirstOrDefaultAsync(d => d.Technologies == _object.SOWRequirement);

            if (sowRequirement == null)
                throw new KeyNotFoundException("SowRequirement not found");

            var employee = await _context.TblEmployee
               .FirstOrDefaultAsync(d => d.Name == _object.Employee);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            sowProposedTeam.SOWRequirementId = sowRequirement?.Id;
            sowProposedTeam.EmployeeId = employee?.Id;
            sowProposedTeam.IsActive = _object.IsActive;
            sowProposedTeam.CreatedBy = _object.CreatedBy;
            sowProposedTeam.CreatedDate = _object.CreatedDate;
            sowProposedTeam.UpdatedBy = _object.UpdatedBy;
            sowProposedTeam.UpdatedDate = _object.UpdatedDate;

            _context.Entry(sowProposedTeam).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            // Check if the technology exists
            var existingsowproposedteam = await _repository.Get(id);
            if (existingsowproposedteam == null)
            {
                throw new ArgumentException($"Technology with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}
