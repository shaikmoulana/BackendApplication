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
    public class ClientRepository : IRepository<Client>
    {
        private readonly DataBaseContext _context;

        public ClientRepository(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAll()
        {
            return await _context.TblClient.ToListAsync();
        }

        public async Task<Client> Get(string id)
        {
            return await _context.TblClient.FindAsync(id);
        }

        public async Task<Client> Create(Client client)
        {
            _context.TblClient.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> Update(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<bool> Delete(string id)
        {
            var client = await _context.TblClient.FindAsync(id);
            if (client == null)
            {
                return false;
            }

            _context.TblClient.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}