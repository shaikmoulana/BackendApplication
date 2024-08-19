using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Repositories
{
    public class ProjectEmployeeRepository : IRepository<ProjectEmployee>
    {
        private readonly DataBaseContext _context;

        public ProjectEmployeeRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectEmployee>> GetAll()
        {
            return await _context.TblProjectEmployee.ToListAsync();
        }

        public async Task<ProjectEmployee> Get(string id)
        {
            return await _context.TblProjectEmployee.FindAsync(id);
        }

        public async Task<ProjectEmployee> Create(ProjectEmployee _object)
        {
            _context.TblProjectEmployee.Add(_object);
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<ProjectEmployee> Update(ProjectEmployee _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            var data = await _context.TblProjectEmployee.FindAsync(id);
            if (data == null)
            {
                return false;
            }

            _context.TblProjectEmployee.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}