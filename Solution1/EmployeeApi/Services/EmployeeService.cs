using DataServices.Models;
using DataServices.Repositories;

namespace EmployeeApi.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IRepository<Employee> _repository;

        public EmployeeService(IRepository<Employee> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Employee> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<Employee> Add(Employee employee)
        {
            /*employee.DepartmentId=employee.DepartmentId;*/
            /*employee.Name = employee.Name;
            employee.DesignationId = employee.DesignationId;
            employee.EmployeeID = employee.EmployeeID;
            employee.EmailId = employee.EmailId;
            employee.DepartmentId = employee.DepartmentId;
            employee.ReportingTo = employee.ReportingTo;
            employee.JoiningDate = employee.JoiningDate;
            employee.RelievingDate = employee.RelievingDate;
            employee.Projection = employee.Projection;
            employee.IsActive = true;
            employee.CreatedBy = "SYSTEM";
            employee.CreatedDate = DateTime.Now;
            employee.UpdatedDate = employee.UpdatedDate;
            employee.UpdatedBy = employee.UpdatedBy;
            employee.Password = employee.Password;
            return await _repository.Create(employee);*/


            // Further processing or transformation if needed

            return await _repository.Create(employee);

        }

        public async Task<Employee> Update(Employee employee)
        {
            // Retrieve the existing employee from the database
            /*var existingEmployee = await _repository.Get(employee.Id);
            if (existingEmployee == null)
            {
                throw new ArgumentException($"Employee with ID {employee.Id} not found.");
            }

            // Update properties with the new values
            existingEmployee.Name = employee.Name;
            existingEmployee.DepartmentId = employee.DepartmentId;
            existingEmployee.UpdatedDate = DateTime.Now;
            existingEmployee.UpdatedBy = employee.UpdatedBy;
            existingEmployee.UpdatedDate = employee.UpdatedDate;
            // Call repository to update the employee
            return await _repository.Update(existingEmployee);*/

            return await _repository.Update(employee);
        }

        public async Task<bool> Delete(string id)
        {
            // Check if the employee exists
            var existingEmployee = await _repository.Get(id);
            if (existingEmployee == null)
            {
                throw new ArgumentException($"Employee with ID {id} not found.");
            }

            // Call repository to delete the employee
            return await _repository.Delete(id);
        }


    }
}
