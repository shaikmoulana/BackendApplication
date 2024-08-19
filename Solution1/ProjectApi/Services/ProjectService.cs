using DataServices.Models;
using DataServices.Repositories;

namespace ProjectApi.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _repository;

        public ProjectService(IRepository<Project> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Project> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<Project> Add(Project _object)
        {
            return await _repository.Create(_object);
        }

        public async Task<Project> Update(Project _object)
        {
            /*// Retrieve the existing technology from the database
            var existingData = await _repository.Get(_object.Id);
            if (existingData == null)
            {
                throw new ArgumentException($" with ID {_object.Id} not found.");
            }

            // Update properties with the new values
            existingData.ClientId = _object.ClientId;
            existingData.ProjectName = _object.ProjectName;
            existingData.TechnicalProjectManager = _object.TechnicalProjectManager;
            existingData.SalesContact= _object.SalesContact;
            existingData.PMO= _object.PMO;
            existingData.SOWSubmittedDate= _object.SOWSubmittedDate;
            existingData.SOWSignedDate= _object.SOWSignedDate;
            existingData.SOWValidTill= _object.SOWValidTill;
            existingData.SOWLastExtendedDate= _object.SOWLastExtendedDate;
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
