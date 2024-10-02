using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.EntityFrameworkCore;

namespace POCAPI.Services
{
    public class POCService : IPOCService
    {
        private readonly IRepository<POC> _repository;
        private readonly DataBaseContext _context;

        public POCService(IRepository<POC> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<POCDTO>> GetAll()
        {
            var pocs = await _context.TblPOC.Include(e => e.Client).ToListAsync();
            var pocDtos = new List<POCDTO>();

            foreach (var poc in pocs)
            {
                pocDtos.Add(new POCDTO
                {
                    Id = poc.Id,
                    Title = poc.Title,
                    Client = poc.Client?.Name,
                    Status = poc.Status,
                    TargetDate = poc.TargetDate,
                    CompletedDate = poc.CompletedDate,
                    Document = poc.Document,
                    IsActive = poc.IsActive,
                    CreatedBy = poc.CreatedBy,
                    CreatedDate = poc.CreatedDate,
                    UpdatedBy = poc.UpdatedBy,
                    UpdatedDate = poc.UpdatedDate
                });
            }
            return pocDtos;
        }

        public async Task<POCDTO> Get(string id)
        {
            var poc = await _context.TblPOC
                .Include(e => e.Client)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (poc == null)
                return null;

            return new POCDTO
            {
                Id = poc.Id,
                Title = poc.Title,
                Client = poc.Client?.Name,
                Status = poc.Status,
                TargetDate = poc.TargetDate,
                CompletedDate = poc.CompletedDate,
                Document = poc.Document,
                IsActive = poc.IsActive,
                CreatedBy = poc.CreatedBy,
                CreatedDate = poc.CreatedDate,
                UpdatedBy = poc.UpdatedBy,
                UpdatedDate = poc.UpdatedDate
            };
        }

        public async Task<POCDTO> Add(POCDTO _object)
        {
            var client = await _context.TblClient
                .FirstOrDefaultAsync(d => d.Name == _object.Client);

            if (client == null)
                throw new KeyNotFoundException("Client not found");

            var poc = new POC
            {
                Title = _object.Title,
                ClientId = client.Id,
                Status = _object?.Status,
                TargetDate = _object.TargetDate,
                CompletedDate = _object.CompletedDate,
                Document = _object.Document,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblPOC.Add(poc);
            await _context.SaveChangesAsync();

            _object.Id = poc.Id;
            return _object;
        }

        public async Task<POCDTO> Update(POCDTO _object)
        {
            var poc = await _context.TblPOC.FindAsync(_object.Id);

            if (poc == null)
                throw new KeyNotFoundException("Poc not found");

            var client = await _context.TblClient
                .FirstOrDefaultAsync(d => d.Name == _object.Client);

            if (client == null)
                throw new KeyNotFoundException("Author not found");

            poc.Title = _object.Title;
            poc.ClientId = client?.Id;
            poc.Status = _object.Status;
            poc.TargetDate = _object.TargetDate;
            poc.CompletedDate = _object.CompletedDate;
            poc.Document= _object.Document;
            poc.IsActive = _object.IsActive;
            poc.CreatedBy = _object.CreatedBy;
            poc.CreatedDate = _object.CreatedDate;
            poc.UpdatedBy = _object.UpdatedBy;
            poc.UpdatedDate = _object.UpdatedDate;

            _context.Entry(poc).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _object;
        }

        public async Task<bool> Delete(string id)
        {
            // Check if the Blogs exists
            var existingData = await _repository.Get(id);
            if (existingData == null)
            {
                throw new ArgumentException($"Blogs with ID {id} not found.");
            }
            existingData.IsActive = false; // Soft delete
            await _repository.Update(existingData); // Save changes
            return true;
        }
    }
}
