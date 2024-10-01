using DataServices.Data;
using DataServices.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Repositories
{
    public class SOWRequirementTechnologyRepository : IRepository<SOWRequirementTechnology>
    {
        private readonly DataBaseContext _context;

        public SOWRequirementTechnologyRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SOWRequirementTechnology>> GetAll()
        {
            return await _context.TblSOWRequirementTechnology.ToListAsync();
        }

        public async Task<SOWRequirementTechnology> Get(string id)
        {
            return await _context.TblSOWRequirementTechnology.FindAsync(id);
        }

        public async Task<SOWRequirementTechnology> Create(SOWRequirementTechnology sowrequirementtechnology)
        {
            _context.TblSOWRequirementTechnology.Add(sowrequirementtechnology);
            await _context.SaveChangesAsync();
            return sowrequirementtechnology;
        }

        public async Task<SOWRequirementTechnology> Update(SOWRequirementTechnology sowrequirementtechnology)
        {
            _context.Entry(sowrequirementtechnology).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return sowrequirementtechnology;
        }

        public async Task<bool> Delete(string id)
        {
            var sowrequirement = await _context.TblSOWRequirementTechnology.FindAsync(id);
            if (sowrequirement == null)
            {
                return false;
            }

            _context.TblSOWRequirementTechnology.Remove(sowrequirement);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
