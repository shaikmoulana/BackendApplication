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
    public class POCRepository : IRepository<POC>
    {
        private readonly DataBaseContext _context;

        public POCRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<POC>> GetAll()
        {
            return await _context.TblPOC.ToListAsync();
        }

        public async Task<POC> Get(string id)
        {
            return await _context.TblPOC.FindAsync(id);
        }

        public async Task<POC> Create(POC poc)
        {
            _context.TblPOC.Add(poc);
            await _context.SaveChangesAsync();
            return poc;
        }

        public async Task<POC> Update(POC poc)
        {
            _context.Entry(poc).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return poc;
        }

        public async Task<bool> Delete(string id)
        {
            var poc = await _context.TblPOC.FindAsync(id);
            if (poc == null)
            {
                return false;
            }

            _context.TblPOC.Remove(poc);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
