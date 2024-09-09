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
            var employees = await _context.TblEmployee.Include(e => e.Department).Include(e => e.Designation).ToListAsync();
            var empDtos = new List<EmployeeDTO>();

            foreach (var employee in employees)
            {
                empDtos.Add(new EmployeeDTO
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Designation = employee.Designation?.Name, // Access Designation Name
                    EmployeeID = employee.EmployeeID,
                    EmailId = employee.EmailId,
                    Department = employee.Department?.Name, // Access Department Name
                    ReportingTo = employee.ReportingTo,
                    JoiningDate = employee.JoiningDate,
                    RelievingDate = employee.RelievingDate,
                    Projection = employee.Projection,
                    IsActive = employee.IsActive,
                    CreatedBy = employee.CreatedBy,
                    CreatedDate = employee.CreatedDate,
                    UpdatedBy = employee.UpdatedBy,
                    UpdatedDate = employee.UpdatedDate,
                    //Password = employee.Password
                    Profile = employee.Profile,
                    PhoneNo = employee.PhoneNo
                });
            }

            return empDtos;
        }

        public async Task<EmployeeDTO> Get(string id)
        {
            var employee = await _context.TblEmployee
                  .Include(e => e.Department)
                  .Include(e => e.Designation)
                  .FirstOrDefaultAsync(t => t.Id == id);
            if (employee == null) return null;

            return new EmployeeDTO
            {
                Id = employee.Id,
                Name = employee.Name,
                Designation = employee.Designation?.Name, // Access Designation Name
                EmployeeID = employee.EmployeeID,
                EmailId = employee.EmailId,
                Department = employee.Department?.Name, // Access Department Name
                ReportingTo = employee.ReportingTo,
                JoiningDate = employee.JoiningDate,
                RelievingDate = employee.RelievingDate,
                Projection = employee.Projection,
                IsActive = employee.IsActive,
                CreatedBy = employee.CreatedBy,
                CreatedDate = employee.CreatedDate,
                UpdatedBy = employee.UpdatedBy,
                UpdatedDate = employee.UpdatedDate,
                //Password = employee.Password
                Profile = employee.Profile,
                PhoneNo = employee.PhoneNo
            };
        }

        public async Task<EmployeeDTO> Add(EmployeeDTO empDto)
        {
            var department = await _context.TblDepartment
               .FirstOrDefaultAsync(d => d.Name == empDto.Department);

            if (department == null)
                throw new KeyNotFoundException("Department not found");

            var designation = await _context.TblDesignation
               .FirstOrDefaultAsync(d => d.Name == empDto.Designation);

            if (designation == null)
                throw new KeyNotFoundException("Designation not found");

            var employee = new Employee
            {
                Name = empDto.Name,
                DesignationId = designation.Id,
                EmployeeID = empDto.EmployeeID,
                EmailId = empDto.EmailId,
                DepartmentId = department.Id,
                ReportingTo = empDto.ReportingTo,
                JoiningDate = empDto.JoiningDate,
                RelievingDate = empDto.RelievingDate,
                Projection = empDto.Projection,
                IsActive = empDto.IsActive,
                CreatedBy = empDto.CreatedBy,
                CreatedDate = empDto.CreatedDate,
                UpdatedBy = empDto.UpdatedBy,
                UpdatedDate = empDto.UpdatedDate,
                Password = PasswordHasher.HashPassword(empDto.Password),
                Profile = empDto.Profile,
                PhoneNo = empDto.PhoneNo
            };

            _context.TblEmployee.Add(employee);
            await _context.SaveChangesAsync();

            empDto.Id = employee.Id;
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

            var designation = await _context.TblDesignation
                .FirstOrDefaultAsync(d => d.Name == empDto.Designation);

            if (designation == null)
                throw new KeyNotFoundException("Designation not found");

            employee.Name = empDto.Name;
            employee.DesignationId = designation.Id;
            employee.EmployeeID = empDto.EmployeeID;
            employee.EmailId = empDto.EmailId;
            employee.DepartmentId = department.Id;
            employee.ReportingTo = empDto.ReportingTo;
            employee.JoiningDate = empDto.JoiningDate;
            employee.RelievingDate = empDto.RelievingDate;
            employee.Projection = empDto.Projection;
            employee.IsActive = empDto.IsActive;
            employee.UpdatedBy = empDto.UpdatedBy;
            employee.UpdatedDate = empDto.UpdatedDate;
            employee.Password = PasswordHasher.HashPassword(empDto.Password);
            employee.Profile = empDto.Profile;
            employee.PhoneNo = empDto.PhoneNo;

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return empDto;
            //return await _repository.Update(employee);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
