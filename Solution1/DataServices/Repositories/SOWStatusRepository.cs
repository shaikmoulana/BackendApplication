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
    public class SOWStatusRepository : IRepository<SOWStatus>
    {
        private readonly DataBaseContext _context;

        public SOWStatusRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SOWStatus>> GetAll()
        {
            return await _context.TblSOWStatus.ToListAsync();
        }

        public async Task<SOWStatus> Get(string id)
        {
            return await _context.TblSOWStatus.FindAsync(id);
        }

        public async Task<SOWStatus> Create(SOWStatus sowstatus)
        {
            _context.TblSOWStatus.Add(sowstatus);
            await _context.SaveChangesAsync();
            return sowstatus;
        }

        public async Task<SOWStatus> Update(SOWStatus sowstatus)
        {
            _context.Entry(sowstatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return sowstatus;
        }

        public async Task<bool> Delete(string id)
        {
            var sowstatus = await _context.TblSOWStatus.FindAsync(id);
            if (sowstatus == null)
            {
                return false;
            }

            _context.TblSOWStatus.Remove(sowstatus);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
