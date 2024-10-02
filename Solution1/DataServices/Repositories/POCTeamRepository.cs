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
    public class POCTeamRepository : IRepository<POCTeam>
    {
        private readonly DataBaseContext _context;

        public POCTeamRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<POCTeam>> GetAll()
        {
            return await _context.TblPOCTeam.ToListAsync();
        }

        public async Task<POCTeam> Get(string id)
        {
            return await _context.TblPOCTeam.FindAsync(id);
        }

        public async Task<POCTeam> Create(POCTeam pocTeam)
        {
            _context.TblPOCTeam.Add(pocTeam);
            await _context.SaveChangesAsync();
            return pocTeam;
        }

        public async Task<POCTeam> Update(POCTeam pocTeam)
        {
            _context.Entry(pocTeam).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return pocTeam;
        }

        public async Task<bool> Delete(string id)
        {
            var pocTeam = await _context.TblPOCTeam.FindAsync(id);
            if (pocTeam == null)
            {
                return false;
            }

            _context.TblPOCTeam.Remove(pocTeam);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
