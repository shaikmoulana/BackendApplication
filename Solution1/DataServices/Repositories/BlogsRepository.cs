using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Repositories
{
    public class BlogsRepository : IRepository<Blogs>
    {
        private readonly DataBaseContext _context;
        private readonly ILogger<Blogs> _logger;
        public BlogsRepository(DataBaseContext context, ILogger<Blogs> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Blogs>> GetAll()
        {
            try
            {
                return await _context.TblBlogs
                                     .Select(b => new Blogs
                                     {
                                         Id = b.Id,
                                         Title = b.Title ?? string.Empty,
                                         Author = b.Author ?? string.Empty,
                                         Status = b.Status ?? string.Empty,
                                         TargetDate = b.TargetDate,
                                         CompletedDate = b.CompletedDate,
                                         PublishedDate = b.PublishedDate,
                                         IsActive = b.IsActive,
                                         CreatedBy = b.CreatedBy ?? "SYSTEM",
                                         CreatedDate = b.CreatedDate,
                                         UpdatedBy = b.UpdatedBy,
                                         UpdatedDate = b.UpdatedDate
                                     })
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all blogs");
                throw;
            }
            //  return await _context.TblBlogs.ToListAsync();
        }

        public async Task<Blogs> Get(string id)
        {
            return await _context.TblBlogs.FindAsync(id);
        }

        public async Task<Blogs> Create(Blogs blogs)
        {
            _context.TblBlogs.Add(blogs);
            await _context.SaveChangesAsync();
            return blogs;
        }

        public async Task<Blogs> Update(Blogs blogs)
        {
            _context.Entry(blogs).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return blogs;
        }

        public async Task<bool> Delete(string id)
        {
            var blogs = await _context.TblBlogs.FindAsync(id);
            if (blogs == null)
            {
                return false;
            }

            _context.TblBlogs.Remove(blogs);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}

