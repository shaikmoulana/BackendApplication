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
    public class SOWRepository : IRepository<SOW>
    {
        private readonly DataBaseContext _context;

        public SOWRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SOW>> GetAll()
        {
            return await _context.TblSOW.ToListAsync();
        }

        public async Task<SOW> Get(string id)
        {
            return await _context.TblSOW.FindAsync(id);
        }

        public async Task<SOW> Create(SOW sow)
        {
            _context.TblSOW.Add(sow);
            await _context.SaveChangesAsync();
            return sow;
        }

        public async Task<SOW> Update(SOW sow)
        {
            _context.Entry(sow).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return sow;
        }

        public async Task<bool> Delete(string id)
        {
            var sow = await _context.TblSOW.FindAsync(id);
            if (sow == null)
            {
                return false;
            }

            _context.TblSOW.Remove(sow);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
