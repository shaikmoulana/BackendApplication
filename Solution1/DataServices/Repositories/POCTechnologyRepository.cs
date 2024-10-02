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
    public class POCTechnologyRepository : IRepository<POCTechnology>
    {
        private readonly DataBaseContext _context;

        public POCTechnologyRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<POCTechnology>> GetAll()
        {
            return await _context.TblPOCTechnology.ToListAsync();
        }

        public async Task<POCTechnology> Get(string id)
        {
            return await _context.TblPOCTechnology.FindAsync(id);
        }

        public async Task<POCTechnology> Create(POCTechnology pocTechnology)
        {
            _context.TblPOCTechnology.Add(pocTechnology);
            await _context.SaveChangesAsync();
            return pocTechnology;
        }

        public async Task<POCTechnology> Update(POCTechnology pocTechnology)
        {
            _context.Entry(pocTechnology).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return pocTechnology;
        }

        public async Task<bool> Delete(string id)
        {
            var pocTechnology = await _context.TblPOCTechnology.FindAsync(id);
            if (pocTechnology == null)
            {
                return false;
            }

            _context.TblPOCTechnology.Remove(pocTechnology);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
