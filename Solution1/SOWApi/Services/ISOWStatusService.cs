using DataServices.Models;

namespace SOWApi.Services
{
    public interface ISOWStatusService
    {
        Task<IEnumerable<SOWStatus>> GetAll();
        Task<SOWStatus> Get(string id);
        Task<SOWStatus> Add(SOWStatus sowstatus);
        Task<SOWStatus> Update(SOWStatus sowstatus);
        Task<bool> Delete(string id);
    }
}
