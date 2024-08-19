/*using DataServices.Data;
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

*/

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
                return await _context.Blogs
                                     .Select(b => new Blogs
                                     {
                                         Id = b.Id,
                                         Title = b.Title ?? string.Empty,
                                         Author = b.Author ?? string.Empty,
                                         Status = b.Status ?? string.Empty,
                                         TargetDate = b.TargetDate.HasValue ? b.TargetDate.Value : (DateTime?)null,
                                         CompletedDate = b.CompletedDate.HasValue ? b.CompletedDate.Value : (DateTime?)null,
                                         PublishedDate = b.PublishedDate.HasValue ? b.PublishedDate.Value : (DateTime?)null,
                                         IsActive = b.IsActive,
                                         CreatedBy = "SYSTEM",
                                         CreatedDate = b.CreatedDate,
                                         UpdatedBy = b.UpdatedBy ?? string.Empty,
                                         UpdatedDate = b.UpdatedDate.HasValue ? b.UpdatedDate.Value : (DateTime?)null
                                     })
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all blogs");
                throw;
            }
        }

        public async Task<Blogs> Get(string id)
        {
            var blog = await _context.Blogs
                .Where(b => b.Id == id)
                .Select(b => new Blogs
                {
                    Id = b.Id,
                    Title = b.Title ?? string.Empty,
                    Author = b.Author ?? string.Empty,
                    Status = b.Status ?? string.Empty,
                    TargetDate = b.TargetDate.HasValue ? b.TargetDate.Value : (DateTime?)null,
                    CompletedDate = b.CompletedDate.HasValue ? b.CompletedDate.Value : (DateTime?)null,
                    PublishedDate = b.PublishedDate.HasValue ? b.PublishedDate.Value : (DateTime?)null,
                    IsActive = b.IsActive,
                    CreatedBy = "SYSTEM",
                    CreatedDate = b.CreatedDate,
                    UpdatedBy = b.UpdatedBy ?? string.Empty,
                    UpdatedDate = b.UpdatedDate.HasValue ? b.UpdatedDate.Value : (DateTime?)null
                })
                .SingleOrDefaultAsync();

            if (blog == null)
            {
                _logger.LogWarning($"Blog with ID {id} not found.");
            }

            return blog;
        }





        public async Task<Blogs> Create(Blogs blogs)
        {
            _context.Blogs.Add(blogs);
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
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                _logger.LogWarning($"Blog with ID {id} not found for deletion.");
                return false;
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}