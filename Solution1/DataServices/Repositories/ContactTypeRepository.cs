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
    public class ContactTypeRepository : IRepository<ContactType>
    {
        private readonly DataBaseContext _context;

        public ContactTypeRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ContactType>> GetAll()
        {
            return await _context.TblContactType.ToListAsync();
        }

        public async Task<ContactType> Get(string id)
        {
            return await _context.TblContactType.FindAsync(id);
        }

        public async Task<ContactType> Create(ContactType _object)
        {
            _context.TblContactType.Add(_object);
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<ContactType> Update(ContactType _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            var data = await _context.TblContactType.FindAsync(id);
            if (data == null)
            {
                return false;
            }

            _context.TblContactType.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}