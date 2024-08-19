using DataServices.Models;

namespace ProjectApi.Services
{
    public interface IProjectTechnologyService
    {
        public Task<IEnumerable<ProjectTechnology>> GetAll();
        public Task<ProjectTechnology> Get(string id);
        public Task<ProjectTechnology> Add(ProjectTechnology _object);
        public Task<ProjectTechnology> Update(ProjectTechnology _object);
        public Task<bool> Delete(string id);
    }
}
