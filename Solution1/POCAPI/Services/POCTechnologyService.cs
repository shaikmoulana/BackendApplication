using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.EntityFrameworkCore;

namespace POCAPI.Services
{
    public class POCTechnologyService : IPOCTechnologyService
    {
        private readonly IRepository<POCTechnology> _repository;
        private readonly DataBaseContext _context;

        public POCTechnologyService(IRepository<POCTechnology> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<POCTechnologyDTO>> GetAll()
        {
            var pocs = await _context.TblPOCTechnology
                .Include(e => e.POC)
                .Include(e => e.Technology)
                .ToListAsync();
            var pocDtos = new List<POCTechnologyDTO>();

            foreach (var poc in pocs)
            {
                pocDtos.Add(new POCTechnologyDTO
                {
                    Id = poc.Id,
                    POC = poc.POC?.Title,
                    Technology = poc.Technology?.Name,
                    IsActive = poc.IsActive,
                    CreatedBy = poc.CreatedBy,
                    CreatedDate = poc.CreatedDate,
                    UpdatedBy = poc.UpdatedBy,
                    UpdatedDate = poc.UpdatedDate
                });
            }
            return pocDtos;
        }

        public async Task<POCTechnologyDTO> Get(string id)
        {
            var poc = await _context.TblPOCTechnology
                .Include(e => e.POC)
                .Include(e => e.Technology)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (poc == null)
                return null;

            return new POCTechnologyDTO
            {
                Id = poc.Id,
                POC = poc.POC?.Title,
                Technology = poc.Technology?.Name,
                IsActive = poc.IsActive,
                CreatedBy = poc.CreatedBy,
                CreatedDate = poc.CreatedDate,
                UpdatedBy = poc.UpdatedBy,
                UpdatedDate = poc.UpdatedDate
            };
        }

        public async Task<POCTechnologyDTO> Add(POCTechnologyDTO _object)
        {
            var poc = await _context.TblPOC
                .FirstOrDefaultAsync(d => d.Title == _object.POC);

            if (poc == null)
                throw new KeyNotFoundException("POC not found");

            var technology = await _context.TblTechnology
                .FirstOrDefaultAsync(d => d.Name == _object.Technology);

            if (technology == null)
                throw new KeyNotFoundException("Technology not found");

            var pocTech = new POCTechnology
            {
                POCId = poc.Id,
                TechnologyId = technology.Id,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblPOCTechnology.Add(pocTech);
            await _context.SaveChangesAsync();

            _object.Id = pocTech.Id;
            return _object;
        }

        public async Task<POCTechnologyDTO> Update(POCTechnologyDTO _object)
        {
            var pocTech = await _context.TblPOCTechnology.FindAsync(_object.Id);

            if (pocTech == null)
                throw new KeyNotFoundException("PocTech not found");

            var poc = await _context.TblPOC
                .FirstOrDefaultAsync(d => d.Title == _object.POC);

            if (poc == null)
                throw new KeyNotFoundException("POC not found");

            var technology = await _context.TblTechnology
                .FirstOrDefaultAsync(d => d.Name == _object.Technology);

            if (technology == null)
                throw new KeyNotFoundException("Technology not found");

            pocTech.POCId = poc.Id;
            pocTech.TechnologyId = technology?.Id;
            pocTech.IsActive = _object.IsActive;
            pocTech.CreatedBy = _object.CreatedBy;
            pocTech.CreatedDate = _object.CreatedDate;
            pocTech.UpdatedBy = _object.UpdatedBy;
            pocTech.UpdatedDate = _object.UpdatedDate;

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
                throw new ArgumentException($"POCTech with ID {id} not found.");
            }
            existingData.IsActive = false; // Soft delete
            await _repository.Update(existingData); // Save changes
            return true;
        }
    }
}
