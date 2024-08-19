using DataServices.Models;

namespace TechnologyApi.Services
{
    public interface ITechnologyService
    {
        Task<IEnumerable<Technology>> GetAll();
        Task<Technology> Get(string id);
        Task<Technology> Add(Technology technology);
        Task<Technology> Update(Technology technology);
        Task<bool> Delete(string id);
    }
}