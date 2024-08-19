using DataServices.Models;

namespace BlogsApi.Services
{
    public interface IBlogsService
    {
        Task<IEnumerable<Blogs>> GetAll();
        Task<Blogs> Get(string id);
        Task<Blogs> Add(Blogs _object);
        Task<Blogs> Update(Blogs _object);
        Task<bool> Delete(string id);
    }
}
