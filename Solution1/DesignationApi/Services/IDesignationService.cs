using DataServices.Models;

namespace DesignationApi.Services
{
    public interface IDesignationService
    {
        public Task<IEnumerable<Designation>> GetAll();
        public Task<Designation> Get(string id);
        public Task<Designation> Add(Designation _object);
        public Task<Designation> Update(Designation _object);
        public Task<bool> Delete(string id);
    }
}
