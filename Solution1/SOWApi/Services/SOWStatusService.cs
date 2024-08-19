using DataServices.Models;
using DataServices.Repositories;

namespace SOWApi.Services
{
    public class SOWStatusService : ISOWStatusService
    {
        private readonly IRepository<SOWStatus> _repository;

        public SOWStatusService(IRepository<SOWStatus> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SOWStatus>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SOWStatus> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<SOWStatus> Add(SOWStatus sowstatus)
        {
            
            /*sowstatus.Status = sowstatus.Status.ToString();
            sowstatus.IsActive = true;
            sowstatus.CreatedDate = DateTime.Now;
            sowstatus.CreatedBy = "SYSTEM";
            sowstatus.UpdatedDate = sowstatus.UpdatedDate;
            sowstatus.UpdatedBy = sowstatus.UpdatedBy;*/
            return await _repository.Create(sowstatus);
        }

        public async Task<SOWStatus> Update(SOWStatus sowstatus)
        {
            // Retrieve the existing technology from the database
            /*var existingsowstatus = await _repository.Get(sowstatus.Id);
            if (existingsowstatus == null)
            {
                throw new ArgumentException($"SowStatus with ID {sowstatus.Id} not found.");
            }

            // Update properties with the new values
            
            existingsowstatus.Status = sowstatus.Status;
            existingsowstatus.UpdatedDate = DateTime.Now;
            existingsowstatus.UpdatedBy = sowstatus.UpdatedBy;
            existingsowstatus.UpdatedDate = sowstatus.UpdatedDate;
            // Call repository to update the technology
            return await _repository.Update(existingsowstatus);*/
            return await _repository.Update(sowstatus);

        }

        public async Task<bool> Delete(string id)
        {
            // Check if the technology exists
            var existingsowstatus = await _repository.Get(id);
            if (existingsowstatus == null)
            {
                throw new ArgumentException($"Technology with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}
