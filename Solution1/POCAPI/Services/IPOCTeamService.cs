using DataServices.Models;

namespace POCAPI.Services
{
    public interface IPOCTeamService
    {
        Task<IEnumerable<POCTeamDTO>> GetAll();
        Task<POCTeamDTO> Get(string id);
        Task<POCTeamDTO> Add(POCTeamDTO _object);
        Task<POCTeamDTO> Update(POCTeamDTO _object);
        Task<bool> Delete(string id);
    }
}
