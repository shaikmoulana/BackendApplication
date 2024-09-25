using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechnologyApi.Services
{
    public class TechnologyService : ITechnologyService
    {
        private readonly DataBaseContext _context;
        private readonly IRepository<Technology> _repository;

        public TechnologyService(DataBaseContext context, IRepository<Technology> repository)
        {
            _context = context;
            _repository = repository;
        }

        public async Task<IEnumerable<TechnologyDTO>> GetAll()
        {
            var technologies = await _context.TblTechnology.Include(t => t.Department).ToListAsync();
            var techDtos = new List<TechnologyDTO>();

            foreach (var tech in technologies)
            {
                techDtos.Add(new TechnologyDTO
                {
                    Id = tech.Id,
                    Name = tech.Name,
                    Department = tech.Department?.Name,
                    IsActive = tech.IsActive,
                    CreatedBy = tech.CreatedBy,
                    CreatedDate = tech.CreatedDate,
                    UpdatedBy = tech.UpdatedBy,
                    UpdatedDate = tech.UpdatedDate
                });
            }

            return techDtos;
        }

        public async Task<TechnologyDTO> Get(string id)
        {
            var technology = await _context.TblTechnology
                .Include(t => t.Department)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (technology == null)
                return null;

            return new TechnologyDTO
            {
                Id = technology.Id,
                Name = technology.Name,
                Department = technology.Department?.Name,
                IsActive = technology.IsActive,
                CreatedBy = technology.CreatedBy,
                CreatedDate = technology.CreatedDate,
                UpdatedBy = technology.UpdatedBy,
                UpdatedDate = technology.UpdatedDate
            };
        }

        public async Task<TechnologyDTO> Add(TechnologyDTO technologyDto)
        {
            var department = await _context.TblDepartment
                .FirstOrDefaultAsync(d => d.Name == technologyDto.Department);

            if (department == null)
                throw new KeyNotFoundException("Department not found");

            var technology = new Technology
            {
                Name = technologyDto.Name,
                DepartmentId = department.Id,
                IsActive = technologyDto.IsActive,
                CreatedBy = technologyDto.CreatedBy,
                CreatedDate = technologyDto.CreatedDate,
                UpdatedBy = technologyDto.UpdatedBy,
                UpdatedDate = technologyDto.UpdatedDate
            };

            _context.TblTechnology.Add(technology);
            await _context.SaveChangesAsync();

            technologyDto.Id = technology.Id;
            return technologyDto;
        }

        public async Task<TechnologyDTO> Update(TechnologyDTO technologyDto)
        {
            var technology = await _context.TblTechnology.FindAsync(technologyDto.Id);

            if (technology == null)
                throw new KeyNotFoundException("Technology not found");

            var department = await _context.TblDepartment
                .FirstOrDefaultAsync(d => d.Name == technologyDto.Department);

            if (department == null)
                throw new KeyNotFoundException("Department not found");

            technology.Name = technologyDto.Name;
            technology.DepartmentId = department.Id;
            technology.IsActive = technologyDto.IsActive;
            technology.UpdatedBy = technologyDto.UpdatedBy;
            technology.UpdatedDate = technologyDto.UpdatedDate;

            _context.Entry(technology).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return technologyDto;
        }

        public async Task<bool> Delete(string id)
        {
            /*var technology = await _context.TblTechnology.FindAsync(id);
            if (technology == null)
                return false;
            _context.TblTechnology.Remove(technology);
            await _context.SaveChangesAsync();
            return true;*/

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