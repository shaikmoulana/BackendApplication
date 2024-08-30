﻿/*using DataServices.Models;
using DataServices.Repositories;

namespace InterviewApi.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly IRepository<Interviews> _repository;
        private readonly ILogger<InterviewService> _logger;

        public InterviewService(IRepository<Interviews> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Interviews>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Interviews> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<Interviews> Create(Interviews interview)
        {
            *//*interview.SOWRequirement = interview.SOWRequirement;
            interview.Name = interview.Name;
            interview.InterviewDate = interview.InterviewDate;
            interview.YearsOfExperience = interview.YearsOfExperience;
            interview.Status = interview.Status;
            interview.On_Boarding = interview.On_Boarding;
            interview.Recruiter = interview.Recruiter;
            interview.IsActive = interview.IsActive;
            interview.CreatedBy = interview.CreatedBy;
            interview.CreatedDate = interview.CreatedDate;
            interview.UpdatedBy = interview.UpdatedBy;
            interview.UpdatedDate = interview.UpdatedDate;*//*
            return await _repository.Create(interview);
        }

        public async Task<Interviews> Update(Interviews interview)
        {
            var existinginterview = await _repository.Get(interview.Id);
            if (existinginterview == null)
            {
                throw new ArgumentException($"Interviews with ID {interview.Id} not found.");
            }
            existinginterview.SOWRequirement = interview.SOWRequirement;
            existinginterview.Name = interview.Name;
            existinginterview.InterviewDate = interview.InterviewDate;
            existinginterview.YearsOfExperience = interview.YearsOfExperience;
            existinginterview.Status = interview.Status;
            existinginterview.On_Boarding = interview.On_Boarding;
            existinginterview.Recruiter = interview.Recruiter;
            existinginterview.IsActive = interview.IsActive;
            existinginterview.CreatedBy = interview.CreatedBy;
            existinginterview.CreatedDate = interview.CreatedDate;
            existinginterview.UpdatedBy = interview.UpdatedBy;
            existinginterview.UpdatedDate = interview.UpdatedDate;
            return await _repository.Update(existinginterview);

        }


        public Task<bool> Delete(string id)
        {
            var existinginterview = _repository.Get(id);
            if (existinginterview == null)
            {
                throw new ArgumentException($"Interviews with ID {id} not found.");
            }
            return _repository.Delete(id);
        }
    }
}
*/


using DataServices.Data;
using DataServices.Models;
using DataServices.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace InterviewApi.Services
{
    public class InterviewService : IInterviewService
    {
        private readonly IRepository<Interviews> _repository;
        private readonly ILogger<InterviewService> _logger;
        private readonly DataBaseContext _context;

        public InterviewService(IRepository<Interviews> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<InterviewsDTO>> GetAll()
        {
            var interviews = await _context.TblInterviews
                 .Include(c => c.SOWRequirements)
                 .Include(t => t.InterviewStatus)
                 .Include(s => s.Employee)
                 .ToListAsync();

            var interviewsDto = new List<InterviewsDTO>();
            foreach (var item in interviews)
            {
                interviewsDto.Add(new InterviewsDTO
                {
                    Id = item.Id,
                    SOWRequirement = item.SOWRequirements?.Technologies,
                    Name = item.Name,
                    InterviewDate = item.InterviewDate,
                    YearsOfExperience = item.YearsOfExperience,
                    Status = item.InterviewStatus?.Status,
                    On_Boarding = item.On_Boarding,
                    Recruiter = item.Employee?.Name,
                    IsActive = item.IsActive,
                    CreatedBy = item.CreatedBy,
                    CreatedDate = item.CreatedDate,
                    UpdatedBy = item.UpdatedBy,
                    UpdatedDate = item.UpdatedDate
                });
            }
            return interviewsDto;
        }

        public async Task<InterviewsDTO> Get(string id)
        {
            var interviews = await _context.TblInterviews
                 .Include(c => c.SOWRequirements)
                 .Include(t => t.InterviewStatus)
                 .Include(s => s.Employee)
                 .FirstOrDefaultAsync(t => t.Id == id);
            if (interviews == null) return null;

            return new InterviewsDTO
            {
                Id = interviews.Id,
                SOWRequirement = interviews.SOWRequirements?.Technologies,
                Name = interviews.Name,
                InterviewDate = interviews.InterviewDate,
                YearsOfExperience = interviews.YearsOfExperience,
                Status = interviews.InterviewStatus?.Status,
                On_Boarding = interviews.On_Boarding,
                Recruiter = interviews.Employee?.Name,
                IsActive = interviews.IsActive,
                CreatedBy = interviews.CreatedBy,
                CreatedDate = interviews.CreatedDate,
                UpdatedBy = interviews.UpdatedBy,
                UpdatedDate = interviews.UpdatedDate
            };
        }

        public async Task<InterviewsDTO> Add(InterviewsDTO _object)
        {
            var sowRequirement = await _context.TblSOWRequirement
               .FirstOrDefaultAsync(d => d.Technologies == _object.SOWRequirement);

            if (sowRequirement == null)
                throw new KeyNotFoundException("SOWRequirement not found");

            var status = await _context.TblInterviewStatus
               .FirstOrDefaultAsync(d => d.Status == _object.Status);

            if (status == null)
                throw new KeyNotFoundException("status not found");

            var recruiter = await _context.TblEmployee
               .FirstOrDefaultAsync(d => d.Name == _object.Recruiter);

            if (recruiter == null)
                throw new KeyNotFoundException("SalesContact not found");


            var interviews = new Interviews
            {
                SOWRequirementId = sowRequirement?.Id,
                Name = _object.Name,
                InterviewDate = _object.InterviewDate,
                YearsOfExperience = _object.YearsOfExperience,
                StatusId = status?.Id,
                On_Boarding = _object.On_Boarding,
                Recruiter = recruiter?.Id,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblInterviews.Add(interviews);
            await _context.SaveChangesAsync();

            _object.Id = interviews.Id;
            return _object;
        }

        public async Task<InterviewsDTO> Update(InterviewsDTO _object)
        {
            var interview = await _context.TblInterviews.FindAsync(_object.Id);

            if (interview == null)
                throw new KeyNotFoundException("Interview not found");

            var sowRequirement = await _context.TblSOWRequirement
               .FirstOrDefaultAsync(d => d.Technologies == _object.SOWRequirement);

            if (sowRequirement == null)
                throw new KeyNotFoundException("SOWRequirement not found");

            var status = await _context.TblInterviewStatus
               .FirstOrDefaultAsync(d => d.Status == _object.Status);

            if (status == null)
                throw new KeyNotFoundException("status not found");

            var recruiter = await _context.TblEmployee
               .FirstOrDefaultAsync(d => d.Name == _object.Recruiter);

            if (recruiter == null)
                throw new KeyNotFoundException("SalesContact not found");

            interview.SOWRequirementId = sowRequirement?.Id;
            interview.Name = _object.Name;
            interview.InterviewDate = _object.InterviewDate;
            interview.YearsOfExperience = _object.YearsOfExperience;
            interview.StatusId = status?.Id;
            interview.On_Boarding = _object.On_Boarding;
            interview.Recruiter = recruiter?.Id;
            interview.IsActive = _object.IsActive;
            interview.CreatedBy = _object.CreatedBy;
            interview.CreatedDate = _object.CreatedDate;
            interview.UpdatedBy = _object.UpdatedBy;
            interview.UpdatedDate = _object.UpdatedDate;

            _context.Entry(interview).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _object;

        }


        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}