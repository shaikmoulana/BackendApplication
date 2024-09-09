using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Services
{
    public class EmployeeTechnologyService : IEmployeeTechnologyService
    {
        private readonly IRepository<EmployeeTechnology> _repository;
        private readonly DataBaseContext _context;
        public EmployeeTechnologyService(IRepository<EmployeeTechnology> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<EmployeeTechnologyDTO>> GetAll()
        {
            var empTechnologies = await _context.TblEmployeeTechnology
                .Include(e => e.Technologies)
                .Include(e => e.Employee)
                .ToListAsync();
            var empTechDtos = new List<EmployeeTechnologyDTO>();

            foreach (var e in empTechnologies)
            {
                empTechDtos.Add(new EmployeeTechnologyDTO()
                {
                    Id = e.Id,
                    EmployeeID = e.Employee?.EmployeeID,
                    Technology = e.Technologies?.Name,
                    IsActive = e.IsActive,
                    CreatedDate = DateTime.Now,
                    CreatedBy = e.CreatedBy,
                    UpdatedDate = DateTime.Now,
                    UpdatedBy = e.UpdatedBy
                });

            }

            return empTechDtos;
        }
        public async Task<EmployeeTechnologyDTO> Get(string id)
        {
            var employeeTechnology = await _context.TblEmployeeTechnology
               .Include(e => e.Technologies)
               .Include(e => e.Employee)
               .FirstOrDefaultAsync(t => t.Id == id);
            if (employeeTechnology == null) return null;

            return new EmployeeTechnologyDTO
            {
                Id = employeeTechnology.Id,
                EmployeeID = employeeTechnology.Employee?.EmployeeID,
                Technology = employeeTechnology.Technologies?.Name,
                IsActive = employeeTechnology.IsActive,
                CreatedDate = DateTime.Now,
                CreatedBy = employeeTechnology.CreatedBy,
                UpdatedDate = DateTime.Now,
                UpdatedBy = employeeTechnology.UpdatedBy
            };
        }

        public async Task<EmployeeTechnologyDTO> Add(EmployeeTechnologyDTO empTechDto)
        {
            var technology = await _context.TblTechnology
                .FirstOrDefaultAsync(t => t.Name == empTechDto.Technology);

            if (technology == null)
                throw new KeyNotFoundException("Technology not found");

            var employee = await _context.TblEmployee
                .FirstOrDefaultAsync(t => t.EmployeeID == empTechDto.EmployeeID);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            var employeeTechnology = new EmployeeTechnology
            {
                EmployeeID = employee.Id,
                Technology = technology.Id,
                IsActive = true,
                CreatedDate = DateTime.Now,
                CreatedBy = empTechDto.CreatedBy,
                UpdatedDate = DateTime.Now,
                UpdatedBy = empTechDto.UpdatedBy
            };

            _context.TblEmployeeTechnology.Add(employeeTechnology);
            await _context.SaveChangesAsync();

            empTechDto.Id = employeeTechnology.Id;
            return empTechDto;

        }

        public async Task<EmployeeTechnologyDTO> Update(EmployeeTechnologyDTO empTechDto)
        {

            var employeeTechnology = await _context.TblEmployeeTechnology.FindAsync(empTechDto.Id);

            if (employeeTechnology == null)
                throw new KeyNotFoundException("EmployeeTechnology not found");

            var technology = await _context.TblTechnology
                .FirstOrDefaultAsync(t => t.Name == empTechDto.Technology);

            if (technology == null)
                throw new KeyNotFoundException("Technology not found");

            var employee = await _context.TblEmployee
                .FirstOrDefaultAsync(t => t.EmployeeID == empTechDto.EmployeeID);

            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            employeeTechnology.EmployeeID = employee.Id;
            employeeTechnology.Technology = technology.Id;
            employeeTechnology.IsActive = true;
            employeeTechnology.CreatedBy = empTechDto.CreatedBy;
            employeeTechnology.CreatedDate = DateTime.Now;
            employeeTechnology.UpdatedBy = empTechDto.UpdatedBy;
            employeeTechnology.UpdatedDate = DateTime.Now;

            _context.Entry(employeeTechnology).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return empTechDto;
        }

        public async Task<bool> Delete(string id)
        {
            // Check if the employee exists
            var existingEmployeeTechnology = await _repository.Get(id);
            if (existingEmployeeTechnology == null)
            {
                throw new ArgumentException($"EmployeeTechnology with ID {id} not found.");
            }

            // Call repository to delete the employee
            return await _repository.Delete(id);
        }

    }
}