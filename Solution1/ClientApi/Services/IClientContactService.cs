using ClientApi.Services;
using DataServices.Models;

namespace ClientServices.Services
{
    public interface IClientContactService
    {

        public Task<IEnumerable<ClientContact>> GetAll();
        public Task<ClientContact> Get(string id);
        public Task<ClientContact> Add(ClientContact _object);
        public Task<ClientContact> Update(ClientContact _object);
        public Task<bool> Delete(string id);

    }
}
