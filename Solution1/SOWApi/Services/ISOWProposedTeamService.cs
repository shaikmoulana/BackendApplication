using DataServices.Models;

namespace SOWApi.Services
{
    public interface ISOWProposedTeamService
    {
        Task<IEnumerable<SOWProposedTeamDTO>> GetAll();
        Task<SOWProposedTeamDTO> Get(string id);
        Task<SOWProposedTeamDTO> Add(SOWProposedTeamDTO sowproposedteam);
        Task<SOWProposedTeamDTO> Update(SOWProposedTeamDTO sowproposedteam);
        Task<bool> Delete(string id);
    }
}
