using DataServices.Models;

namespace ProjectApi.Services
{
    public interface IProjectEmployeeService
    {
        public Task<IEnumerable<ProjectEmployee>> GetAll();
        public Task<ProjectEmployee> Get(string id);
        public Task<ProjectEmployee> Add(ProjectEmployee _object);
        public Task<ProjectEmployee> Update(ProjectEmployee _object);
        public Task<bool> Delete(string id);
    }
}
