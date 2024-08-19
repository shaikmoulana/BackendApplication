using DataServices.Models;

namespace SOWApi.Services
{
    public interface ISOWService
    {
        Task<IEnumerable<SOW>> GetAll();
        Task<SOW> Get(string id);
        Task<SOW> Add(SOW sow);
        Task<SOW> Update(SOW sow);
        Task<bool> Delete(string id);
    }
}
