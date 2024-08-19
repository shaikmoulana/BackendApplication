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
    public class SOWProposedTeamRepository : IRepository<SOWProposedTeam>
    {
        private readonly DataBaseContext _context;

        public SOWProposedTeamRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SOWProposedTeam>> GetAll()
        {
            return await _context.TblSOWProposedTeam.ToListAsync();
        }

        public async Task<SOWProposedTeam> Get(string id)
        {
            return await _context.TblSOWProposedTeam.FindAsync(id);
        }

        public async Task<SOWProposedTeam> Create(SOWProposedTeam sowproposedteam)
        {
            _context.TblSOWProposedTeam.Add(sowproposedteam);
            await _context.SaveChangesAsync();
            return sowproposedteam;
        }

        public async Task<SOWProposedTeam> Update(SOWProposedTeam sowproposedteam)
        {
            _context.Entry(sowproposedteam).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return sowproposedteam;
        }

        public async Task<bool> Delete(string id)
        {
            var sowproposedteam = await _context.TblSOWProposedTeam.FindAsync(id);
            if (sowproposedteam == null)
            {
                return false;
            }

            _context.TblSOWProposedTeam.Remove(sowproposedteam);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
