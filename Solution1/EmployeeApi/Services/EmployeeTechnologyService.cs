
using DataServices.Models;
using DataServices.Repositories;

namespace EmployeeApi.Services
{
    public class EmployeeTechnologyService : IEmployeeTechnologyService
    {
        private readonly IRepository<EmployeeTechnology> _repository;
        public EmployeeTechnologyService(IRepository<EmployeeTechnology> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<EmployeeTechnology>> GetAll()
        {
            return await _repository.GetAll();
        }
        public async Task<EmployeeTechnology> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<EmployeeTechnology> Add(EmployeeTechnology employeeTechnology)
        {
            return await _repository.Create(employeeTechnology);
        }

        public async Task<EmployeeTechnology> Update(EmployeeTechnology employeeTechnology)
        {
            // Retrieve the existing employee from the database
/*            var existingEmployeeTechnology = await _repository.Get(employeeTechnology.Id);
            if (existingEmployeeTechnology == null)
            {
                throw new ArgumentException($"Employee with ID {employeeTechnology.Id} not found.");
            }

            // Update properties with the new values
            existingEmployeeTechnology.Name = employeeTechnology.Name;
            existingEmployeeTechnology.TechnologyId = employeeTechnology.TechnologyId;
            existingEmployeeTechnology.UpdatedDate = DateTime.Now;
            existingEmployeeTechnology.UpdatedBy = employeeTechnology.UpdatedBy;
            existingEmployeeTechnology.UpdatedDate = employeeTechnology.UpdatedDate;
            // Call repository to update the employeeTechnology
            return await _repository.Update(existingEmployeeTechnology);*/
            return await _repository.Update(employeeTechnology);
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
