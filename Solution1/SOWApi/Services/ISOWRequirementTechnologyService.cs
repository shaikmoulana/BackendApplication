using DataServices.Models;

namespace SOWApi.Services
{
    public interface ISOWRequirementTechnologyService
    {
        Task<IEnumerable<SOWRequirementTechnologyDTO>> GetAll();
        Task<SOWRequirementTechnologyDTO> Get(string id);
        Task<SOWRequirementTechnologyDTO> Add(SOWRequirementTechnologyDTO sowrequirementtechnology);
        Task<SOWRequirementTechnologyDTO> Update(SOWRequirementTechnologyDTO sowrequirementtechnology);
        Task<bool> Delete(string id);
    }
}
