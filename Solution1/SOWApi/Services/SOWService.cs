using DataServices.Models;
using DataServices.Repositories;

namespace SOWApi.Services
{
    public class SOWService : ISOWService
    {
        private readonly IRepository<SOW> _repository;

        public SOWService(IRepository<SOW> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SOW>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<SOW> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<SOW> Add(SOW sow)
        {
            
/*            sow.Client = sow.Client;
            sow.Project = sow.Project;
            sow.PreparedDate= DateTime.Now;
            sow.SubmittedDate= DateTime.Now;
            sow.Status = sow.Status;
            sow.Comments = sow.Comments;
            sow.CreatedDate = DateTime.Now;
            sow.IsActive = true;
            sow.CreatedBy = "SYSTEM";
            sow.UpdatedDate = sow.UpdatedDate;
            sow.UpdatedBy = sow.UpdatedBy;*/
            return await _repository.Create(sow);
        }

        public async Task<SOW> Update(SOW sow)
        {
            // Retrieve the existing technology from the database
            /*var existingsow = await _repository.Get(sow.Id);
            if (existingsow== null)
            {
                throw new ArgumentException($"SOW with ID {sow.Id} not found.");
            }

            // Update properties with the new values
            existingsow.Client = sow.Client;
            existingsow.Project = sow.Project;
            existingsow.PreparedDate = sow.PreparedDate;
            existingsow.SubmittedDate = sow.SubmittedDate;
            existingsow.Status = sow.Status;
            existingsow.Comments = sow.Comments;
            existingsow.CreatedBy = "SYSTEM";
            existingsow.CreatedDate = DateTime.Now;
            existingsow.UpdatedBy = sow.UpdatedBy;
            existingsow.UpdatedDate = sow.UpdatedDate;
            // Call repository to update the technology
            return await _repository.Update(existingsow);*/
            return await _repository.Update(sow);
        }

        public async Task<bool> Delete(string id)
        {
            // Check if the technology exists
            var existingsow = await _repository.Get(id);
            if (existingsow == null)
            {
                throw new ArgumentException($"Technology with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}
