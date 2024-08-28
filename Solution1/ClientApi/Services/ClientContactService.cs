/*using ClientServices.Services;
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
*/


using ClientServices.Services;
using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ClientApi.Services
{
    public class ClientContactService : IClientContactService
    {
        private readonly IRepository<ClientContact> _repository;
        private readonly DataBaseContext _context;

        public ClientContactService(IRepository<ClientContact> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<ClientContactDTO>> GetAll()
        {
            var clientContacts = await _context.TblClientContact.Include(c => c.Client).ToListAsync();
            var ccDTO = new List<ClientContactDTO>();

            foreach (var contact in clientContacts)
            {
                ccDTO.Add(new ClientContactDTO
                {
                    Id = contact.Id,
                    Client = contact.Client?.Name,
                    ContactValue = contact.ContactValue,
                    ContactType = contact.ContactType,
                    IsActive = contact.IsActive,
                    CreatedBy = contact.CreatedBy,
                    CreatedDate = contact.CreatedDate,
                    UpdatedBy = contact.UpdatedBy,
                    UpdatedDate = contact.UpdatedDate
                });
            }

            return ccDTO;
        }

        public async Task<ClientContactDTO> Get(string id)
        {
            var clientContact = await _context.TblClientContact
                .Include(c => c.Client)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (clientContact == null)
                return null;

            return new ClientContactDTO
            {
                Id = clientContact.Id,
                Client = clientContact.Client?.Name,
                ContactValue = clientContact.ContactValue,
                ContactType = clientContact.ContactType,
                IsActive = clientContact.IsActive,
                CreatedBy = clientContact.CreatedBy,
                CreatedDate = clientContact.CreatedDate,
                UpdatedBy = clientContact.UpdatedBy,
                UpdatedDate = clientContact.UpdatedDate
            };

        }

        public async Task<ClientContactDTO> Add(ClientContactDTO _object)
        {
            var client = await _context.TblClient
                .FirstOrDefaultAsync(c => c.Name == _object.Client);

            if (client == null)
                throw new KeyNotFoundException("Client not found");

            var clientContact = new ClientContact
            {
                ClientId = client.Id,
                ContactValue = _object.ContactValue,
                ContactType = _object.ContactType,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblClientContact.Add(clientContact);
            await _context.SaveChangesAsync();

            _object.Id = clientContact.Id;
            return _object;

        }

        public async Task<ClientContactDTO> Update(ClientContactDTO _object)
        {
            var clientContact = await _context.TblClientContact.FindAsync(_object.Id);

            if (clientContact == null)
                throw new KeyNotFoundException("ClientContact not found");

            var client = await _context.TblClient
                .FirstOrDefaultAsync(d => d.Name == _object.Client);

            if (client == null)
                throw new KeyNotFoundException("Client not found");

            clientContact.ClientId = client.Id;
            clientContact.ContactValue = _object.ContactValue;
            clientContact.ContactType = _object.ContactType;
            clientContact.IsActive = _object.IsActive;
            clientContact.CreatedBy = _object.CreatedBy;
            clientContact.CreatedDate = _object.CreatedDate;
            clientContact.UpdatedBy = _object.UpdatedBy;
            clientContact.UpdatedDate = _object.UpdatedDate;

            _context.Entry(clientContact).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            // Call repository to delete the technology
            return await _repository.Delete(id);
        }
    }
}



