using DataServices.Models;
using DataServices.Repositories;
using TechnologyApi.Services;

namespace TechnologyService.Services
{
    public class TechnologyServices : ITechnologyService
    {
        private readonly IRepository<Technology> _repository;

        public TechnologyServices(IRepository<Technology> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Technology>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Technology> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<Technology> Add(Technology technology)
        {
            return await _repository.Create(technology);
        }

        public async Task<Technology> Update(Technology technology)
        {
           return await _repository.Update(technology);
        }

        public async Task<bool> Delete(string id)
        {
            // Check if the technology exists
            var existingTechnology = await _repository.Get(id);
            if (existingTechnology == null)
            {
                throw new ArgumentException($"Technology with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}