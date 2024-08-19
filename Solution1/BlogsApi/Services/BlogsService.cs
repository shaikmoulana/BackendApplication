using DataServices.Models;
using DataServices.Repositories;

namespace BlogsApi.Services
{
    public class BlogsService : IBlogsService
    {
        private readonly IRepository<Blogs> _repository;

        public BlogsService(IRepository<Blogs> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Blogs>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Blogs> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<Blogs> Add(Blogs _object)
        {
            return await _repository.Create(_object);
        }

        public async Task<Blogs> Update(Blogs _object)
        {
            // Retrieve the existing technology from the database
            var existingData = await _repository.Get(_object.Id);
            if (existingData == null)
            {
                throw new ArgumentException($" with ID {_object.Id} not found.");
            }

            // Update properties with the new values
            existingData.Title = _object.Title;
            existingData.Author = _object.Author;
            existingData.Status = _object.Status;
            existingData.TargetDate = _object.TargetDate;
            existingData.CompletedDate = _object.CompletedDate;
            existingData.PublishedDate = _object.PublishedDate;
            existingData.CreatedBy = "SYSTEM";
            existingData.CreatedDate = DateTime.Now;
            existingData.UpdatedBy = _object.UpdatedBy;
            existingData.UpdatedDate = _object.UpdatedDate;
            // Call repository to update the technology
            return await _repository.Update(existingData);
        }

        public async Task<bool> Delete(string id)
        {
            // Check if the technology exists
            var existingData = await _repository.Get(id);
            if (existingData == null)
            {
                throw new ArgumentException($"Technology with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}
