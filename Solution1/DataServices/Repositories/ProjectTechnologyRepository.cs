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
    public class ProjectTechnologyRepository : IRepository<ProjectTechnology>
    {
        private readonly DataBaseContext _context;

        public ProjectTechnologyRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectTechnology>> GetAll()
        {
            return await _context.TblProjectTechnology.ToListAsync();
        }

        public async Task<ProjectTechnology> Get(string id)
        {
            return await _context.TblProjectTechnology.FindAsync(id);
        }

        public async Task<ProjectTechnology> Create(ProjectTechnology _object)
        {
            _context.TblProjectTechnology.Add(_object);
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<ProjectTechnology> Update(ProjectTechnology _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            var data = await _context.TblProjectTechnology.FindAsync(id);
            if (data == null)
            {
                return false;
            }

            _context.TblProjectTechnology.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}