using DataServices.Models;

namespace ProjectApi.Services
{
    public interface IProjectTechnologyService
    {
        public Task<IEnumerable<ProjectTechnologyDTO>> GetAll();
        public Task<ProjectTechnologyDTO> Get(string id);
        public Task<ProjectTechnologyDTO> Add(ProjectTechnologyDTO _object);
        public Task<ProjectTechnologyDTO> Update(ProjectTechnologyDTO _object);
        public Task<bool> Delete(string id);
    }
}
