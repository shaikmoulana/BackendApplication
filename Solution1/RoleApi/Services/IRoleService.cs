using DataServices.Models;

namespace RoleApi.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAll();
    }
}
