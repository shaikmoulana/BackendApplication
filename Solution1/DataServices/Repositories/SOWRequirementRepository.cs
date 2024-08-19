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
    public class SOWRequirementRepository : IRepository<SOWRequirement>
    {
        private readonly DataBaseContext _context;

        public SOWRequirementRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SOWRequirement>> GetAll()
        {
            return await _context.TblSOWRequirement.ToListAsync();
        }

        public async Task<SOWRequirement> Get(string id)
        {
            return await _context.TblSOWRequirement.FindAsync(id);
        }

        public async Task<SOWRequirement> Create(SOWRequirement sowrequirement)
        {
            _context.TblSOWRequirement.Add(sowrequirement);
            await _context.SaveChangesAsync();
            return sowrequirement;
        }

        public async Task<SOWRequirement> Update(SOWRequirement sowrequirement)
        {
            _context.Entry(sowrequirement).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return sowrequirement;
        }

        public async Task<bool> Delete(string id)
        {
            var sowrequirement = await _context.TblSOWRequirement.FindAsync(id);
            if (sowrequirement == null)
            {
                return false;
            }

            _context.TblSOWRequirement.Remove(sowrequirement);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
