using DataServices.Models;

namespace ProjectApi.Services
{
    public interface IProjectEmployeeService
    {
        public Task<IEnumerable<ProjectEmployeeDTO>> GetAll();
        public Task<ProjectEmployeeDTO> Get(string id);
        public Task<ProjectEmployeeDTO> Add(ProjectEmployeeDTO _object);
        public Task<ProjectEmployeeDTO> Update(ProjectEmployeeDTO _object);
        public Task<bool> Delete(string id);
    }
}
