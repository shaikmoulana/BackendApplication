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
    public class WebinarsRepository : IRepository<Webinars>
    {
        private readonly DataBaseContext _context;

        public WebinarsRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Webinars>> GetAll()
        {
            return await _context.TblWebinars.ToListAsync();
        }

        public async Task<Webinars> Get(string id)
        {
            return await _context.TblWebinars.FindAsync(id);
        }

        public async Task<Webinars> Create(Webinars _object)
        {
            _context.TblWebinars.Add(_object);
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<Webinars> Update(Webinars _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            var data = await _context.TblWebinars.FindAsync(id);
            if (data == null)
            {
                return false;
            }

            _context.TblWebinars.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}