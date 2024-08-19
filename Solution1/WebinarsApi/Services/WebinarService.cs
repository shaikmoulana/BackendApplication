using DataServices.Models;
using DataServices.Repositories;

namespace WebinarsApi.Services
{
    public class WebinarService : IWebinarService
    {
        private readonly IRepository<Webinars> _repository;

        public WebinarService(IRepository<Webinars> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Webinars>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Webinars> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<Webinars> Add(Webinars _object)
        {
            return await _repository.Create(_object);
        }

        public async Task<Webinars> Update(Webinars _object)
        {
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
