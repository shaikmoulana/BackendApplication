using ClientServices.Services;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ClientApi.Services
{
    public class ContactTypeService : IContactTypeService
    {
        private readonly IRepository<ContactType> _repository;

        public ContactTypeService(IRepository<ContactType> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ContactType>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ContactType> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<ContactType> Add(ContactType _object)
        {
            return await _repository.Create(_object);
        }

        public async Task<ContactType> Update(ContactType _object)
        {
            return await _repository.Update(_object);
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




