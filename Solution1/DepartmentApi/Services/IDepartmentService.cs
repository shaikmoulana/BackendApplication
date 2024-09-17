using DataServices.Models;

namespace DepartmentApi.Services
{
    public interface IDepartmentService
    {
        public Task<IEnumerable<DepartmentDTO>> GetAll();
        public Task<DepartmentDTO> Get(string id);
        public Task<DepartmentDTO> Add(DepartmentDTO _object);
        public Task<DepartmentDTO> Update(DepartmentDTO _object);
        public Task<bool> Delete(string id);

    }

}
