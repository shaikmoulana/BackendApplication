using DataServices.Models;

namespace SOWApi.Services
{
    public interface ISOWRequirementService
    {
        Task<IEnumerable<SOWRequirementDTO>> GetAll();
        Task<SOWRequirementDTO> Get(string id);
        Task<SOWRequirementDTO> Add(SOWRequirementDTO sowrequirement);
        Task<SOWRequirementDTO> Update(SOWRequirementDTO sowrequirement);
        Task<bool> Delete(string id);
    }
}
