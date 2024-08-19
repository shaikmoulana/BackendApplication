using DataServices.Models;

namespace SOWApi.Services
{
    public interface ISOWRequirementService
    {
        Task<IEnumerable<SOWRequirement>> GetAll();
        Task<SOWRequirement> Get(string id);
        Task<SOWRequirement> Add(SOWRequirement sowrequirement);
        Task<SOWRequirement> Update(SOWRequirement sowrequirement);
        Task<bool> Delete(string id);
    }
}
