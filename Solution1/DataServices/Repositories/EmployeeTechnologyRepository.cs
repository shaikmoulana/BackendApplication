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
    public class EmployeeTechnologyRepository : IRepository<EmployeeTechnology>
    {
        private readonly DataBaseContext _context;

        public EmployeeTechnologyRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EmployeeTechnology>> GetAll()
        {
            return await _context.TblEmployeeTechnology.ToListAsync();
        }

        public async Task<EmployeeTechnology> Get(string id)
        {
            return await _context.TblEmployeeTechnology.FindAsync(id);
        }

        public async Task<EmployeeTechnology> Create(EmployeeTechnology _object)
        {
            _context.TblEmployeeTechnology.Add(_object);
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<EmployeeTechnology> Update(EmployeeTechnology _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            var data = await _context.TblEmployeeTechnology.FindAsync(id);
            if (data == null)
            {
                return false;
            }

            _context.TblEmployeeTechnology.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}