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
    public class InterviewStatusRepository : IRepository<InterviewStatus>
    {
        private readonly DataBaseContext _context;

        public InterviewStatusRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InterviewStatus>> GetAll()
        {
            return await _context.TblInterviewStatus.ToListAsync();
        }

        public async Task<InterviewStatus> Get(string id)
        {
            return await _context.TblInterviewStatus.FindAsync(id);
        }

        public async Task<InterviewStatus> Create(InterviewStatus _object)
        {
            _context.TblInterviewStatus.Add(_object);
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<InterviewStatus> Update(InterviewStatus _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            var data = await _context.TblInterviewStatus.FindAsync(id);
            if (data == null)
            {
                return false;
            }

            _context.TblInterviewStatus.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}