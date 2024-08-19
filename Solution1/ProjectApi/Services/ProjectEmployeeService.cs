using DataServices.Models;
using DataServices.Repositories;

namespace ProjectApi.Services
{
    public class ProjectEmployeeService : IProjectEmployeeService
    {
        private readonly IRepository<ProjectEmployee> _repository;

        public ProjectEmployeeService(IRepository<ProjectEmployee> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProjectEmployee>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ProjectEmployee> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<ProjectEmployee> Add(ProjectEmployee _object)
        {
            return await _repository.Create(_object);
        }

        public async Task<ProjectEmployee> Update(ProjectEmployee _object)
        {
            // Retrieve the existing technology from the database
            /*var existingData = await _repository.Get(_object.Id);
            if (existingData == null)
            {
                throw new ArgumentException($" with ID {_object.Id} not found.");
            }

            // Update properties with the new values
            existingData.Project = _object.Project;
            existingData.Employee = _object.Employee;
            existingData.StartDate = _object.StartDate;
            existingData.EndDate = _object.EndDate;
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
