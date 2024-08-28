using ClientApi.Services;
using DataServices.Models;

namespace ClientServices.Services
{
    public interface IClientContactService
    {

        public Task<IEnumerable<ClientContactDTO>> GetAll();
        public Task<ClientContactDTO> Get(string id);
        public Task<ClientContactDTO> Add(ClientContactDTO _object);
        public Task<ClientContactDTO> Update(ClientContactDTO _object);
        public Task<bool> Delete(string id);

    }
}
