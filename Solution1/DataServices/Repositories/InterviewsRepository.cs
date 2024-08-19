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
    public class InterviewsRepository : IRepository<Interviews>
    {
        private readonly DataBaseContext _context;

        public InterviewsRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Interviews>> GetAll()
        {
            return await _context.TblInterviews.ToListAsync();
        }

        public async Task<Interviews> Get(string id)
        {
            return await _context.TblInterviews.FindAsync(id);
        }

        public async Task<Interviews> Create(Interviews _object)
        {
            _context.TblInterviews.Add(_object);
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<Interviews> Update(Interviews _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            var data = await _context.TblInterviews.FindAsync(id);
            if (data == null)
            {
                return false;
            }

            _context.TblInterviews.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}