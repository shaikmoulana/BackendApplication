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
    public class DesignationRepository : IRepository<Designation>
    {
        private readonly DataBaseContext _context;

        public DesignationRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Designation>> GetAll()
        {
            return await _context.TblDesignation.ToListAsync();
        }

        public async Task<Designation> Get(string id)
        {
            return await _context.TblDesignation.FindAsync(id);
        }

        public async Task<Designation> Create(Designation _object)
        {
            _context.TblDesignation.Add(_object);
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<Designation> Update(Designation _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            var data = await _context.TblDesignation.FindAsync(id);
            if (data == null)
            {
                return false;
            }

            _context.TblDesignation.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}