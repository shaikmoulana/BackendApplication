using DataServices.Models;
using DataServices.Repositories;

namespace SOWApi.Services
{
    public class SOWProposedTeamService : ISOWProposedTeamService
    {
        private readonly IRepository<SOWProposedTeam> _repository;

        public SOWProposedTeamService(IRepository<SOWProposedTeam> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SOWProposedTeam>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SOWProposedTeam> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<SOWProposedTeam> Add(SOWProposedTeam sowproposedteam)
        {
            
            /*sowproposedteam.SOWRequirement = sowproposedteam.SOWRequirement;
            sowproposedteam.Employee = sowproposedteam.Employee;
            sowproposedteam.IsActive = true;
            sowproposedteam.CreatedBy = "SYSTEM";
            sowproposedteam.CreatedDate = DateTime.Now;
            sowproposedteam.UpdatedDate = sowproposedteam.UpdatedDate;
            sowproposedteam.UpdatedBy = sowproposedteam.UpdatedBy;*/
            return await _repository.Create(sowproposedteam);
        }

        public async Task<SOWProposedTeam> Update(SOWProposedTeam sowproposedteam)
        {
            // Retrieve the existing technology from the database
            /*var existingsowproposedteam = await _repository.Get(sowproposedteam.Id);
            if (existingsowproposedteam == null)
            {
                throw new ArgumentException($"Technology with ID {sowproposedteam.Id} not found.");
            }

            // Update properties with the new values
            existingsowproposedteam.SOWRequirement = sowproposedteam.SOWRequirement;
            existingsowproposedteam.Employee = sowproposedteam.Employee;
            existingsowproposedteam.UpdatedDate = DateTime.Now;
            existingsowproposedteam.UpdatedBy = sowproposedteam.UpdatedBy;
            existingsowproposedteam.UpdatedDate = sowproposedteam.UpdatedDate;
            // Call repository to update the technology
            return await _repository.Update(existingsowproposedteam);*/
            return await _repository.Update(sowproposedteam);
        }

        public async Task<bool> Delete(string id)
        {
            // Check if the technology exists
            var existingsowproposedteam = await _repository.Get(id);
            if (existingsowproposedteam == null)
            {
                throw new ArgumentException($"Technology with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}
