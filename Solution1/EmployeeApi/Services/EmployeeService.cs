using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;
        private readonly DataBaseContext _context;

        public EmployeeService(IRepository<Employee> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<EmployeeDTO>> GetAll()
        {
            var employees = await _context.TblEmployee
                .Include(e => e.Department)
                .Include(e => e.Designation)
                .Include(e => e.Roles)
                .ToListAsync();

            var empDtos = employees.Select(employee => new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Designation = employee.Designation?.Name,
                EmployeeID = employee.EmployeeID,
                EmailId = employee.EmailId,
                Department = employee.Department?.Name,
                ReportingTo = employee.ReportingTo,
                JoiningDate = employee.JoiningDate,
                RelievingDate = employee.RelievingDate,
                Projection = employee.Projection,
                IsActive = employee.IsActive,
                CreatedBy = employee.CreatedBy,
                CreatedDate = employee.CreatedDate,
                UpdatedBy = employee.UpdatedBy,
                UpdatedDate = employee.UpdatedDate,
                Profile = employee.Profile,
                PhoneNo = employee.PhoneNo,
                Role = employee.Role
            }).ToList();

            return empDtos;
        }

        public async Task<EmployeeDTO> Get(string id)
        {
            var employee = await _context.TblEmployee
                  .Include(e => e.Department)
                  .Include(e => e.Designation)
                  .Include(e => e.Roles)
                  .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null) return null;

            return new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Designation = employee.Designation?.Name,
                EmployeeID = employee.EmployeeID,
                EmailId = employee.EmailId,
                Department = employee.Department?.Name,
                ReportingTo = employee.ReportingTo,
                JoiningDate = employee.JoiningDate,
                RelievingDate = employee.RelievingDate,
                Projection = employee.Projection,
                IsActive = employee.IsActive,
                CreatedBy = employee.CreatedBy,
                CreatedDate = employee.CreatedDate,
                UpdatedBy = employee.UpdatedBy,
                UpdatedDate = employee.UpdatedDate,
                Profile = employee.Profile,
                PhoneNo = employee.PhoneNo,
                Role = employee.Role
            };
        }

        public async Task<EmployeeDTO> Add(EmployeeDTO empDto)
        {
            var employee = new Employee();
            
            var department = await _context.TblDepartment
               .FirstOrDefaultAsync(d => d.Name == empDto.Department);
            if (department == null)
                throw new KeyNotFoundException("Department not found");

            var designation = await _context.TblDesignation
               .FirstOrDefaultAsync(d => d.Name == empDto.Designation);
            if (designation == null)
                throw new KeyNotFoundException("Designation not found");

            var reportingTo = await _context.TblEmployee
                .FirstOrDefaultAsync(d => d.Name == empDto.ReportingTo);
            if (reportingTo == null)
                throw new KeyNotFoundException("ReportingTo not found");

            var role = await _context.TblRole
               .FirstOrDefaultAsync(r => r.RoleName == empDto.Role);
            if (role == null)
                throw new KeyNotFoundException("Role not found");

            employee.Name = empDto.Name;
            employee.DesignationId = designation.Id;
            employee.EmployeeID = empDto.EmployeeID;
            employee.EmailId = empDto.EmailId;
            employee.DepartmentId = department.Id;
            employee.ReportingTo = reportingTo.Id;
            employee.JoiningDate = empDto.JoiningDate;
            employee.RelievingDate = empDto.RelievingDate;
            employee.Projection = empDto.Projection;
            employee.IsActive = empDto.IsActive;
            employee.CreatedBy = empDto.CreatedBy;
            employee.CreatedDate = empDto.CreatedDate;
            employee.UpdatedBy = empDto.UpdatedBy;
            employee.UpdatedDate = empDto.UpdatedDate;
            employee.Password = PasswordHasher.HashPassword(empDto.Password);
            employee.Profile = empDto.Profile;
            employee.PhoneNo = empDto.PhoneNo;
            employee.Role = role.Id;

            _context.TblEmployee.Add(employee);
            await _context.SaveChangesAsync();
            empDto.Id = employee.Id;

            if (empDto.Technology != null && empDto.Technology.Any())
            {
                foreach (var technologyId in empDto.Technology)
                {
                    var employeeTechnology = new EmployeeTechnology
                    {
                        EmployeeID = employee.Id,
                        Technology = technologyId.ToString(),
                    };

                    await _context.TblEmployeeTechnology.AddAsync(employeeTechnology);
                }

                await _context.SaveChangesAsync();
            }

            
            return empDto;
        }

        public async Task<EmployeeDTO> Update(EmployeeDTO empDto)
        {
            var employee = await _context.TblEmployee.FindAsync(empDto.Id);
            if (employee == null)
                throw new KeyNotFoundException("Employee not found");

            var department = await _context.TblDepartment
                .FirstOrDefaultAsync(d => d.Name == empDto.Department);
            if (department == null)
                throw new KeyNotFoundException("Department not found");

            var reportingTo = await _context.TblEmployee
                .FirstOrDefaultAsync(d => d.Name == empDto.ReportingTo);
            if (reportingTo == null)
                throw new KeyNotFoundException("ReportingTo not found");

            var designation = await _context.TblDesignation
                .FirstOrDefaultAsync(d => d.Name == empDto.Designation);
            if (designation == null)
                throw new KeyNotFoundException("Designation not found");

            var role = await _context.TblRole
               .FirstOrDefaultAsync(r => r.RoleName == empDto.Role);
            if (role == null)
                throw new KeyNotFoundException("Role not found");

            employee.Name = empDto.Name;
            employee.DesignationId = designation.Id;
            employee.EmployeeID = empDto.EmployeeID;
            employee.EmailId = empDto.EmailId;
            employee.DepartmentId = department.Id;
            employee.ReportingTo = reportingTo.Id;
            employee.JoiningDate = empDto.JoiningDate;
            employee.RelievingDate = empDto.RelievingDate;
            employee.Projection = empDto.Projection;
            employee.IsActive = empDto.IsActive;
            employee.CreatedBy = empDto.CreatedBy;
            employee.CreatedDate = empDto.CreatedDate;
            employee.UpdatedBy = empDto.UpdatedBy;
            employee.UpdatedDate = empDto.UpdatedDate;
            employee.Password = PasswordHasher.HashPassword(empDto.Password);
            employee.Profile = empDto.Profile;
            employee.PhoneNo = empDto.PhoneNo;
            employee.Role = empDto.Role;

            _context.Entry(employee).State = EntityState.Modified;

            if (empDto.Technology != null && empDto.Technology.Any())
            {
                // Remove old technologies
                var employeeTechnologies = await _context.TblEmployeeTechnology
                    .Where(et => et.EmployeeID == empDto.Id)
                    .ToListAsync();
                _context.TblEmployeeTechnology.RemoveRange(employeeTechnologies);

                // Add updated technologies
                foreach (var technologyId in empDto.Technology)
                {
                    var employeeTechnology = new EmployeeTechnology
                    {
                        EmployeeID = employee.Id,
                        Technology = technologyId.ToString(),
                    };

                    await _context.TblEmployeeTechnology.AddAsync(employeeTechnology);
                }
            }

            await _context.SaveChangesAsync();

            return empDto;
        }

        public async Task<bool> Delete(string id)
        {
            var employee = await _context.TblEmployee.FindAsync(id);
            if (employee == null) return false;

            _context.TblEmployee.Remove(employee);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
