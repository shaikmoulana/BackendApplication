using DataServices.Models;
using DataServices.Repositories;

namespace ProjectApi.Services
{
    public class ProjectTechnologyService : IProjectTechnologyService
    {
        private readonly IRepository<ProjectTechnology> _repository;

        public ProjectTechnologyService(IRepository<ProjectTechnology> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProjectTechnology>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ProjectTechnology> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<ProjectTechnology> Add(ProjectTechnology _object)
        {
            return await _repository.Create(_object);
        }

        public async Task<ProjectTechnology> Update(ProjectTechnology _object)
        {
            // Retrieve the existing technology from the database
            /*var existingData = await _repository.Get(_object.Id);
            if (existingData == null)
            {
                throw new ArgumentException($" with ID {_object.Id} not found.");
            }

            // Update properties with the new values
            existingData.Project = _object.Project;
            existingData.Technology = _object.Technology;
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


