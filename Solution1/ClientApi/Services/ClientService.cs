using ClientServices.Services;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ClientApi.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _repository;

        public ClientService(IRepository<Client> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Client> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<Client> Add(Client _object)
        {
            return await _repository.Create(_object);
        }

        public async Task<Client> Update(Client _object)
        {
            // Retrieve the existing technology from the database
            /*var existingData = await _repository.Get(_object.Id);
            if (existingData == null)
            {
                throw new ArgumentException($"ClientContact with ID {_object.Id} not found.");
            }

            // Update properties with the new values
            existingData.Name = _object.Name;
            existingData.LineofBusiness = _object.LineofBusiness;
            existingData.SalesEmployee = _object.SalesEmployee;
            existingData.Country = _object.Country;
            existingData.City = _object.City;
            existingData.State = _object.State;
            existingData.Address = _object.Address;
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
                throw new ArgumentException($"Technology with ID {id} not found.");
            }

            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}




