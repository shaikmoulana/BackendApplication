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
    public class DepartmentRepository : IRepository<Department>
    {
        private readonly DataBaseContext _context;

        public DepartmentRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Department>> GetAll()
        {
            return await _context.TblDepartment.ToListAsync();
        }

        public async Task<Department> Get(string id)
        {
            return await _context.TblDepartment.FindAsync(id);
        }

        public async Task<Department> Create(Department _object)
        {
            _context.TblDepartment.Add(_object);
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<Department> Update(Department _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            var data = await _context.TblDepartment.FindAsync(id);
            if (data == null)
            {
                return false;
            }

            _context.TblDepartment.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}