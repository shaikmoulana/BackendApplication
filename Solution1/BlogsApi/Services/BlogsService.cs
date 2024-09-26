﻿using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace BlogsApi.Services
{
    public class BlogsService : IBlogsService
    {
        private readonly IRepository<Blogs> _repository;
        private readonly DataBaseContext _context;

        public BlogsService(IRepository<Blogs> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<BlogsDTO>> GetAll()
        {
            var blogs = await _context.TblBlogs.Include(e => e.Employee).ToListAsync();
            var blogsDtos = new List<BlogsDTO>();

            foreach (var blog in blogs)
            {
                blogsDtos.Add(new BlogsDTO
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Author = blog.Employee?.Name,
                    Status = blog.Status,
                    TargetDate = blog.TargetDate,
                    CompletedDate = blog.CompletedDate,
                    PublishedDate = blog.PublishedDate,
                    IsActive = blog.IsActive,
                    CreatedBy = blog.CreatedBy,
                    CreatedDate = blog.CreatedDate,
                    UpdatedBy = blog.UpdatedBy,
                    UpdatedDate = blog.UpdatedDate
                });
            }
            return blogsDtos;
        }

        public async Task<BlogsDTO> Get(string id)
        {
            var blog = await _context.TblBlogs
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (blog == null)
                return null;

            return new BlogsDTO
            {
                Id = blog.Id,
                Title = blog.Title,
                Author = blog.Employee?.Name,
                Status = blog.Status,
                TargetDate = blog.TargetDate,
                CompletedDate = blog.CompletedDate,
                PublishedDate = blog.PublishedDate,
                IsActive = blog.IsActive,
                CreatedBy = blog.CreatedBy,
                CreatedDate = blog.CreatedDate,
                UpdatedBy = blog.UpdatedBy,
                UpdatedDate = blog.UpdatedDate
            };
        }

        public async Task<BlogsDTO> Add(BlogsDTO _object)
        {
            var author = await _context.TblEmployee
                .FirstOrDefaultAsync(d => d.Name == _object.Author);

            if (author == null)
                throw new KeyNotFoundException("Author not found");

            var blog = new Blogs
            {
                Title = _object.Title,
                Author = author.Id,
                Status = _object?.Status,
                TargetDate = _object.TargetDate,
                CompletedDate = _object.CompletedDate,
                PublishedDate = _object.PublishedDate,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblBlogs.Add(blog);
            await _context.SaveChangesAsync();

            _object.Id = blog.Id;
            return _object;
        }

        public async Task<BlogsDTO> Update(BlogsDTO _object)
        {
            var blog = await _context.TblBlogs.FindAsync(_object.Id);

            if (blog == null)
                throw new KeyNotFoundException("Client not found");

            var author = await _context.TblEmployee
                .FirstOrDefaultAsync(d => d.Name == _object.Author);

            if (author == null)
                throw new KeyNotFoundException("Author not found");

            blog.Title = _object.Title;
            blog.Author = author?.Id;
            blog.Status = _object.Status;
            blog.TargetDate = _object.TargetDate;
            blog.CompletedDate = _object.CompletedDate;
            blog.PublishedDate = _object.PublishedDate;
            blog.IsActive = _object.IsActive;
            blog.CreatedBy = _object.CreatedBy;
            blog.CreatedDate = _object.CreatedDate;
            blog.UpdatedBy = _object.UpdatedBy;
            blog.UpdatedDate = _object.UpdatedDate;

            _context.Entry(blog).State = EntityState.Modified;
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