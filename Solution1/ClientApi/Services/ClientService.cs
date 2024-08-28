/*using ClientServices.Services;
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
            *//*var existingData = await _repository.Get(_object.Id);
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
            return await _repository.Update(existingData);*//*
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
*/

using ClientServices.Services;
using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Net;

namespace ClientApi.Services
{
    public class ClientService : IClientService
    {
        private readonly IRepository<Client> _repository;
        private readonly DataBaseContext _context;

        public ClientService(IRepository<Client> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<ClientDTO>> GetAll()
        {
            var clients = await _context.TblClient.Include(e => e.Employee).ToListAsync();
            var clientDtos = new List<ClientDTO>();

            foreach (var client in clients)
            {
                clientDtos.Add(new ClientDTO
                {
                    Id = client.Id,
                    Name = client.Name,
                    LineofBusiness = client.LineofBusiness,
                    SalesEmployee = client.Employee?.Name,
                    Country = client.Country,
                    City = client.City,
                    State = client.State,
                    Address = client.Address,
                    IsActive = client.IsActive,
                    CreatedBy = client.CreatedBy,
                    CreatedDate = client.CreatedDate,
                    UpdatedBy = client.UpdatedBy,
                    UpdatedDate = client.UpdatedDate
                });
            }
            return clientDtos;
        }

        public async Task<ClientDTO> Get(string id)
        {
            var client = await _context.TblClient
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (client == null)
                return null;

            return new ClientDTO
            {
                Id = client.Id,
                Name = client.Name,
                LineofBusiness = client.LineofBusiness,
                SalesEmployee = client.Employee?.Name,
                Country = client.Country,
                City = client.City,
                State = client.State,
                Address = client.Address,
                IsActive = client.IsActive,
                CreatedBy = client.CreatedBy,
                CreatedDate = client.CreatedDate,
                UpdatedBy = client.UpdatedBy,
                UpdatedDate = client.UpdatedDate
            };
        }

        public async Task<ClientDTO> Add(ClientDTO _object)
        {
            var salesEmployee = await _context.TblEmployee
                .FirstOrDefaultAsync(d => d.Name == _object.SalesEmployee);

            if (salesEmployee == null)
                throw new KeyNotFoundException("SalesEmployee not found");

            var client = new Client
            {
                Name = _object.Name,
                LineofBusiness = _object.LineofBusiness,
                SalesEmployee = salesEmployee.Id,
                Country = _object.Country,
                City = _object.City,
                State = _object.State,
                Address = _object.Address,
                IsActive = _object.IsActive,
                CreatedBy= _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblClient.Add(client);
            await _context.SaveChangesAsync();

            _object.Id = client.Id;
            return _object;
        }

        public async Task<ClientDTO> Update(ClientDTO _object)
        {

            var client = await _context.TblClient.FindAsync(_object.Id);

            if (client == null)
                throw new KeyNotFoundException("Client not found");

            var salesEmployee = await _context.TblEmployee
                .FirstOrDefaultAsync(d => d.Name == _object.SalesEmployee);

            if (salesEmployee == null)
                throw new KeyNotFoundException("SalesEmployee not found");

            client.Name = _object.Name;
            client.LineofBusiness = _object.LineofBusiness;
            client.SalesEmployee = salesEmployee.Id;
            client.Country = _object.Country;
            client.City = _object.City;
            client.State = _object.State;
            client.Address = _object.Address;
            client.IsActive = _object.IsActive;
            client.CreatedBy = _object.CreatedBy;
            client.CreatedDate = _object.CreatedDate;
            client.UpdatedBy = _object.UpdatedBy;
            client.UpdatedDate = _object.UpdatedDate;

            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _object;

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



