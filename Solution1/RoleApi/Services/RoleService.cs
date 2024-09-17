using DataServices.Data;
using DataServices.Models;
using Microsoft.EntityFrameworkCore;

namespace RoleApi.Services
{
    public class RoleService : IRoleService
    {
        private readonly DataBaseContext _context;

        public RoleService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleDTO>> GetAll()
        {
            var roles = await _context.TblRole.ToListAsync();
            var roleDtos = new List<RoleDTO>();

            foreach (var r in roles)
            {
                roleDtos.Add(new RoleDTO
                {
                    Id = r.Id,
                    RoleName = r.RoleName
                });
            }

            return roleDtos;
        }
    }
}
