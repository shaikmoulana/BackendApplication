using DataServices.Models;
using DataServices.Repositories;

namespace DesignationApi.Services
{
    public class DesignationService : IDesignationService
    {
        private readonly IRepository<Designation> _repository;

        public DesignationService(IRepository<Designation> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Designation>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Designation> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<Designation> Add(Designation _object)
        {
            return await _repository.Create(_object);
        }

        public async Task<Designation> Update(Designation _object)
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
