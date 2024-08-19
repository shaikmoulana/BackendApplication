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
    public class ClientContactRepository : IRepository<ClientContact>
    {
        private readonly DataBaseContext _context;

        public ClientContactRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ClientContact>> GetAll()
        {
            return await _context.TblClientContact.ToListAsync();
        }

        public async Task<ClientContact> Get(string id)
        {
            return await _context.TblClientContact.FindAsync(id);
        }

        public async Task<ClientContact> Create(ClientContact _object)
        {
            _context.TblClientContact.Add(_object);
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<ClientContact> Update(ClientContact _object)
        {
            _context.Entry(_object).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            var data = await _context.TblClientContact.FindAsync(id);
            if (data == null)
            {
                return false;
            }

            _context.TblClientContact.Remove(data);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}