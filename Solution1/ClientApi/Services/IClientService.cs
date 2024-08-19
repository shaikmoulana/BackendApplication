using DataServices.Models;

namespace ClientServices.Services
{
    public interface IClientService
    {
        public Task<IEnumerable<Client>> GetAll();
        public Task<Client> Get(string id);
        public Task<Client> Add(Client client);
        public Task<Client> Update(Client client);
        public Task<bool> Delete(string id);

    }

}
