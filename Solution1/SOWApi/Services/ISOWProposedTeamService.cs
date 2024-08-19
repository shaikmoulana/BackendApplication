using DataServices.Models;

namespace SOWApi.Services
{
    public interface ISOWProposedTeamService
    {
        Task<IEnumerable<SOWProposedTeam>> GetAll();
        Task<SOWProposedTeam> Get(string id);
        Task<SOWProposedTeam> Add(SOWProposedTeam sowproposedteam);
        Task<SOWProposedTeam> Update(SOWProposedTeam sowproposedteam);
        Task<bool> Delete(string id);
    }
}
