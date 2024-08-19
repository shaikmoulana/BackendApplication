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
    public class ProjectRepository : IRepository<Project>
    {
        private readonly DataBaseContext _context;

        public ProjectRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Project>> GetAll()
        {
            return await _context.TblProject.ToListAsync();
        }

        public async Task<Project> Get(string id)
        {
            return await _context.TblProject.FindAsync(id);
        }

        public async Task<Project> Create(Project _object)
        {
            _context.TblProject.Add(_object);
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<Project> Update(Project _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            var data = await _context.TblProject.FindAsync(id);
            if (data == null)
            {
                return false;
            }

            _context.TblProject.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}