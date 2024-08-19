using ClientServices.Services;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ClientApi.Services
{
    public class ClientContactService : IClientContactService
    {
        private readonly IRepository<ClientContact> _repository;

        public ClientContactService(IRepository<ClientContact> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClientContact>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<ClientContact> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<ClientContact> Add(ClientContact _object)
        {
            return await _repository.Create(_object);
        }

        public async Task<ClientContact> Update(ClientContact _object)
        {
            return await _repository.Update(_object);
        }

        public async Task<bool> Delete(string id)
        {
          // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}




