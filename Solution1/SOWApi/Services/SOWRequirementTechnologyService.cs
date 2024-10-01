using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.EntityFrameworkCore;

namespace SOWApi.Services
{
    public class SOWRequirementTechnologyService : ISOWRequirementTechnologyService
    {
        private readonly IRepository<SOWRequirementTechnology> _repository;
        private readonly DataBaseContext _context;

        public SOWRequirementTechnologyService(IRepository<SOWRequirementTechnology> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<SOWRequirementTechnologyDTO>> GetAll()
        {
            var sowrequirements = await _context.TblSOWRequirementTechnology
               .Include(c => c.SOWs)
               .Include(t => t.Technology)
               .ToListAsync();

            var sowRequirementDto = new List<SOWRequirementTechnologyDTO>();
            foreach (var item in sowrequirements)
            {
                sowRequirementDto.Add(new SOWRequirementTechnologyDTO
                {
                    Id = item.Id,
                    SOW = item.SOWs?.Title,
                    Technology = item.Technology?.Name,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate
                });
            }
            return sowRequirementDto;
        }

        public async Task<SOWRequirementTechnologyDTO> Get(string id)
        {
            var sowRequirements = await _context.TblSOWRequirementTechnology
                .Include(c => c.SOWs)
               .Include(t => t.Technology)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (sowRequirements == null) return null;

            return new SOWRequirementTechnologyDTO
            {
                Id = sowRequirements.Id,
                SOW = sowRequirements.SOWs?.Title,
                Technology = sowRequirements.Technology?.Name,
                IsActive = sowRequirements.IsActive,
                CreatedBy = sowRequirements.CreatedBy,
                CreatedDate = sowRequirements.CreatedDate,
                UpdatedBy = sowRequirements.UpdatedBy,
                UpdatedDate = sowRequirements.UpdatedDate
            };
        }

        public async Task<SOWRequirementTechnologyDTO> Add(SOWRequirementTechnologyDTO _object)
        {

            var sow = await _context.TblSOW
              .FirstOrDefaultAsync(d => d.Title == _object.SOW);

            if (sow == null)
                throw new KeyNotFoundException("Sow not found");

            var technology = await _context.TblTechnology
               .FirstOrDefaultAsync(d => d.Name == _object.Technology);

            if (technology == null)
                throw new KeyNotFoundException("Technology not found");


            var sowRequirement = new SOWRequirementTechnology
            {
                SOWId = sow?.Id,
                TechnologyId = technology?.Id,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblSOWRequirementTechnology.Add(sowRequirement);
            await _context.SaveChangesAsync();

            _object.Id = sowRequirement.Id;
            return _object;
        }

        public async Task<SOWRequirementTechnologyDTO> Update(SOWRequirementTechnologyDTO _object)
        {
            var sowRequirement = await _context.TblSOWRequirementTechnology.FindAsync(_object.Id);

            if (sowRequirement == null)
                throw new KeyNotFoundException("SOWRequirementTechnology not found");

            var sow = await _context.TblSOW
              .FirstOrDefaultAsync(d => d.Title == _object.SOW);

            if (sow == null)
                throw new KeyNotFoundException("Sow not found");

            var technology = await _context.TblTechnology
               .FirstOrDefaultAsync(d => d.Name == _object.Technology);

            if (technology == null)
                throw new KeyNotFoundException("Technology not found");

            sowRequirement.SOWId = sow?.Id;
            sowRequirement.TechnologyId = technology?.Id;
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
                throw new ArgumentException($"SOWRequirementTechnology with ID {id} not found.");
            }
            existingsowrequirement.IsActive = false; // Soft delete
            await _repository.Update(existingsowrequirement); // Save changes
            return true;
        }
    }
}
