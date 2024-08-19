using DataServices.Models;

namespace ProjectApi.Services
{
    public interface IProjectService
    {
        public Task<IEnumerable<Project>> GetAll();
        public Task<Project> Get(string id);
        public Task<Project> Add(Project _object);
        public Task<Project> Update(Project _object);
        public Task<bool> Delete(string id);
    }
}
