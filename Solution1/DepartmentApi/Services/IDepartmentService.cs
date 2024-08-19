using DataServices.Models;

namespace DepartmentApi.Services
{
    public interface IDepartmentService
    {
        public Task<IEnumerable<Department>> GetAll();
        public Task<Department> Get(string id);
        public Task<Department> Add(Department _object);
        public Task<Department> Update(Department _object);
        public Task<bool> Delete(string id);

    }

}
