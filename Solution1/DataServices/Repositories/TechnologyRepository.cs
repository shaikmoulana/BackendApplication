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
    public class TechnologyRepository : IRepository<Technology>
    {
        private readonly DataBaseContext _context;

        public TechnologyRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Technology>> GetAll()
        {
            return await _context.TblTechnology.ToListAsync();
        }

        public async Task<Technology> Get(string id)
        {
            return await _context.TblTechnology.FindAsync(id);
        }

        public async Task<Technology> Create(Technology technology)
        {
            _context.TblTechnology.Add(technology);
            await _context.SaveChangesAsync();
            return technology;
        }

        public async Task<Technology> Update(Technology technology)
        {
            _context.Entry(technology).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return technology;
        }

        public async Task<bool> Delete(string id)
        {
            var technology = await _context.TblTechnology.FindAsync(id);
            if (technology == null)
            {
                return false;
            }

            _context.TblTechnology.Remove(technology);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}