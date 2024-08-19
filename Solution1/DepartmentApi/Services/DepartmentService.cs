using DataServices.Models;
using DataServices.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DepartmentApi.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IRepository<Department> _repository;

        public DepartmentService(IRepository<Department> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Department> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<Department> Add(Department _object)
        {
            return await _repository.Create(_object);
        }

        public async Task<Department> Update(Department _object)
        {
            // Retrieve the existing technology from the database
            /*var existingData = await _repository.Get(_object.Id);
            if (existingData == null)
            {
                throw new ArgumentException($" with ID {_object.Id} not found.");
            }

            // Update properties with the new values
            existingData.Name = _object.Name;
            existingData.IsActive = true;
            existingData.CreatedBy = "SYSTEM";
            existingData.CreatedDate = DateTime.Now;
            existingData.UpdatedBy = _object.UpdatedBy;
            existingData.UpdatedDate = _object.UpdatedDate;
            // Call repository to update the technology
            return await _repository.Update(existingData);*/

            return await _repository.Update(_object);
        }

        public async Task<bool> Delete(string id)
        {
            // Check if the technology exists
            var existingData = await _repository.Get(id);
            if (existingData == null)
            {
                throw new ArgumentException($"with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}




