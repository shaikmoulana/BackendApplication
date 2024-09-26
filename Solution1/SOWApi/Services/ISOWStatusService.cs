using DataServices.Models;

namespace SOWApi.Services
{
    public interface ISOWStatusService
    {
        Task<IEnumerable<SOWStatusDTO>> GetAll();
        Task<SOWStatusDTO> Get(string id);
        Task<SOWStatusDTO> Add(SOWStatusDTO sowstatus);
        Task<SOWStatusDTO> Update(SOWStatusDTO sowstatus);
        Task<bool> Delete(string id);
    }
}
