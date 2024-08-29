/*using DataServices.Models;
using DataServices.Repositories;

namespace SOWApi.Services
{
    public class SOWRequirementService : ISOWRequirementService
    {
        private readonly IRepository<SOWRequirement> _repository;

        public SOWRequirementService(IRepository<SOWRequirement> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SOWRequirement>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SOWRequirement> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<SOWRequirement> Add(SOWRequirement sowrequirement)
        {
         
            *//*sowrequirement.SOW = sowrequirement.SOW;
            sowrequirement.Designation = sowrequirement.Designation;
            sowrequirement.Technologies = sowrequirement.Technologies;
            sowrequirement.TeamSize = sowrequirement.TeamSize;
            sowrequirement.IsActive = true;
            sowrequirement.CreatedBy = "SYSTEM";
            sowrequirement.CreatedDate = DateTime.Now;
            sowrequirement.UpdatedDate = sowrequirement.UpdatedDate;
            sowrequirement.UpdatedBy = sowrequirement.UpdatedBy;*//*
            return await _repository.Create(sowrequirement);
        }

        public async Task<SOWRequirement> Update(SOWRequirement sowrequirement)
        {
            // Retrieve the existing technology from the database
            *//*var existingsowrequirement = await _repository.Get(sowrequirement.Id);
            if (existingsowrequirement == null)
            {
                throw new ArgumentException($"Technology with ID {sowrequirement.Id} not found.");
            }

            // Update properties with the new values
            existingsowrequirement.SOW = sowrequirement.SOW;
            existingsowrequirement.Designation= sowrequirement.Designation;
            existingsowrequirement.Technologies = sowrequirement.Technologies;
            existingsowrequirement.TeamSize = sowrequirement.TeamSize;
            existingsowrequirement.IsActive = true;
            existingsowrequirement.CreatedBy = "SYSTEM";
            existingsowrequirement.CreatedDate = DateTime.Now;
            existingsowrequirement.UpdatedBy = sowrequirement.UpdatedBy;
            existingsowrequirement.UpdatedDate = sowrequirement.UpdatedDate;
            // Call repository to update the technology
            return await _repository.Update(existingsowrequirement);*//*
            return await _repository.Update(sowrequirement);

        }

        public async Task<bool> Delete(string id)
        {
            // Check if the technology exists
            var existingsowrequirement = await _repository.Get(id);
            if (existingsowrequirement == null)
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
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace SOWApi.Services
{
    public class SOWRequirementService : ISOWRequirementService
    {
        private readonly IRepository<SOWRequirement> _repository;
        private readonly DataBaseContext _context;

        public SOWRequirementService(IRepository<SOWRequirement> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<SOWRequirementDTO>> GetAll()
        {
            var sowrequirements = await _context.TblSOWRequirement
               .Include(c => c.SOWs)
               .Include(t => t.Designation)
               .ToListAsync();

            var sowRequirementDto = new List<SOWRequirementDTO>();
            foreach (var item in sowrequirements)
            {
                sowRequirementDto.Add(new SOWRequirementDTO
                {
                    Id = item.Id,
                    SOW = item.SOWs?.Comments,
                    Designation = item.Designation?.Name,
                    Technologies = item.Technologies,
                    TeamSize = item.TeamSize,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate
                });
            }
            return sowRequirementDto;
        }

        public async Task<SOWRequirementDTO> Get(string id)
        {
            var sowRequirements = await _context.TblSOWRequirement
                .Include(c => c.SOWs)
               .Include(t => t.Designation)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (sowRequirements == null) return null;

            return new SOWRequirementDTO
            {
                Id = sowRequirements.Id,
                SOW = sowRequirements.SOWs?.Comments,
                Designation = sowRequirements.Designation?.Name,
                Technologies = sowRequirements.Technologies,
                TeamSize = sowRequirements.TeamSize,
                IsActive = sowRequirements.IsActive,
                CreatedBy = sowRequirements.CreatedBy,
                CreatedDate = sowRequirements.CreatedDate,
                UpdatedBy = sowRequirements.UpdatedBy,
                UpdatedDate = sowRequirements.UpdatedDate
            };
        }

        public async Task<SOWRequirementDTO> Add(SOWRequirementDTO _object)
        {

            var sow = await _context.TblSOW
              .FirstOrDefaultAsync(d => d.Comments == _object.SOW);

            if (sow == null)
                throw new KeyNotFoundException("Sow not found");

            var designation = await _context.TblDesignation
               .FirstOrDefaultAsync(d => d.Name == _object.Designation);

            if (designation == null)
                throw new KeyNotFoundException("Designation not found");


            var sowRequirement = new SOWRequirement
            {
                SOW = sow?.Id,
                DesignationId = designation?.Id,
                Technologies = _object.Technologies,
                TeamSize = _object.TeamSize,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblSOWRequirement.Add(sowRequirement);
            await _context.SaveChangesAsync();

            _object.Id = sowRequirement.Id;
            return _object;
        }

        public async Task<SOWRequirementDTO> Update(SOWRequirementDTO _object)
        {
            var sowRequirement = await _context.TblSOWRequirement.FindAsync(_object.Id);

            if (sowRequirement == null)
                throw new KeyNotFoundException("SOWRequirement not found");

            var sow = await _context.TblSOW
              .FirstOrDefaultAsync(d => d.Comments == _object.SOW);

            if (sow == null)
                throw new KeyNotFoundException("Sow not found");

            var designation = await _context.TblDesignation
               .FirstOrDefaultAsync(d => d.Name == _object.Designation);

            if (designation == null)
                throw new KeyNotFoundException("Designation not found");

            sowRequirement.SOW = sow?.Id;
            sowRequirement.DesignationId = designation?.Id;
            sowRequirement.Technologies = _object.Technologies;
            sowRequirement.TeamSize = _object.TeamSize;
            sowRequirement.IsActive = _object.IsActive;
            sowRequirement.CreatedBy = _object.CreatedBy;
            sowRequirement.CreatedDate = _object.CreatedDate;
            sowRequirement.UpdatedBy = _object.UpdatedBy;
            sowRequirement.UpdatedDate = _object.UpdatedDate;

            _context.Entry(sowRequirement).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _object;

        }

        public async Task<bool> Delete(string id)
        {
            // Check if the technology exists
            var existingsowrequirement = await _repository.Get(id);
            if (existingsowrequirement == null)
            {
                throw new ArgumentException($"Technology with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}