using DataServices.Models;
using DataServices.Repositories;

namespace SOWApi.Services
{
    public class SOWRequirementService : ISOWRequirementService
    {
        private readonly IRepository<SOWRequirement> _repository;

        public SOWRequirementService(IRepository<SOWRequirement> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SOWRequirement>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SOWRequirement> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<SOWRequirement> Add(SOWRequirement sowrequirement)
        {
         
            /*sowrequirement.SOW = sowrequirement.SOW;
            sowrequirement.Designation = sowrequirement.Designation;
            sowrequirement.Technologies = sowrequirement.Technologies;
            sowrequirement.TeamSize = sowrequirement.TeamSize;
            sowrequirement.IsActive = true;
            sowrequirement.CreatedBy = "SYSTEM";
            sowrequirement.CreatedDate = DateTime.Now;
            sowrequirement.UpdatedDate = sowrequirement.UpdatedDate;
            sowrequirement.UpdatedBy = sowrequirement.UpdatedBy;*/
            return await _repository.Create(sowrequirement);
        }

        public async Task<SOWRequirement> Update(SOWRequirement sowrequirement)
        {
            // Retrieve the existing technology from the database
            /*var existingsowrequirement = await _repository.Get(sowrequirement.Id);
            if (existingsowrequirement == null)
            {
                throw new ArgumentException($"Technology with ID {sowrequirement.Id} not found.");
            }

            // Update properties with the new values
            existingsowrequirement.SOW = sowrequirement.SOW;
            existingsowrequirement.Designation= sowrequirement.Designation;
            existingsowrequirement.Technologies = sowrequirement.Technologies;
            existingsowrequirement.TeamSize = sowrequirement.TeamSize;
            existingsowrequirement.IsActive = true;
            existingsowrequirement.CreatedBy = "SYSTEM";
            existingsowrequirement.CreatedDate = DateTime.Now;
            existingsowrequirement.UpdatedBy = sowrequirement.UpdatedBy;
            existingsowrequirement.UpdatedDate = sowrequirement.UpdatedDate;
            // Call repository to update the technology
            return await _repository.Update(existingsowrequirement);*/
            return await _repository.Update(sowrequirement);

        }

        public async Task<bool> Delete(string id)
        {
            // Check if the technology exists
            var existingsowrequirement = await _repository.Get(id);
            if (existingsowrequirement == null)
            {
                throw new ArgumentException($"Technology with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}
