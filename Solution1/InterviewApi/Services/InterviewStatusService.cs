/*using DataServices.Models;
using DataServices.Repositories;

namespace InterviewApi.Services
{
    public class InterviewStatusService : IInterviewStatusService
    {
        private readonly IRepository<InterviewStatus> _repository;
        private readonly ILogger<InterviewStatusService> _logger;

        public InterviewStatusService(IRepository<InterviewStatus> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<InterviewStatus>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<InterviewStatus> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<InterviewStatus> Create(InterviewStatus interviewStatus)
        {
            interviewStatus.Status = interviewStatus.Status;
            interviewStatus.IsActive = interviewStatus.IsActive;
            interviewStatus.CreatedBy = interviewStatus.CreatedBy;
            interviewStatus.CreatedDate = DateTime.Now;
            interviewStatus.UpdatedBy = interviewStatus.UpdatedBy;
            interviewStatus.UpdatedDate = DateTime.Now;
            return await _repository.Create(interviewStatus);
        }

        public async Task<InterviewStatus> Update(InterviewStatus interviewStatus)
        {
            var existinginterviewStatus = await _repository.Get(interviewStatus.Id);
            if (existinginterviewStatus == null)
            {
                throw new ArgumentException($"Interviews with ID {interviewStatus.Id} not found.");
            }
            existinginterviewStatus.Status = interviewStatus.Status;            
            existinginterviewStatus.IsActive = interviewStatus.IsActive;
            existinginterviewStatus.CreatedBy = interviewStatus.CreatedBy;
            existinginterviewStatus.CreatedDate = DateTime.Now;
            existinginterviewStatus.UpdatedBy = interviewStatus.UpdatedBy;
            existinginterviewStatus.UpdatedDate = DateTime.Now;
            return await _repository.Update(existinginterviewStatus);

        }


        public Task<bool> Delete(string id)
        {
            var existinginterviewStatus = _repository.Get(id);
            if (existinginterviewStatus == null)
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
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace InterviewApi.Services
{
    public class InterviewStatusService : IInterviewStatusService
    {
        private readonly IRepository<InterviewStatus> _repository;
        private readonly DataBaseContext _context;

        public InterviewStatusService(IRepository<InterviewStatus> repository, DataBaseContext context)
        {
            _repository = repository;
            _context = context;
        }

        public async Task<IEnumerable<InterviewStatusDTO>> GetAll()
        {
            var interviews = await _context.TblInterviewStatus.ToListAsync();
            if (interviews == null) return null;

            var interviewsDto = new List<InterviewStatusDTO>();
            foreach (var interview in interviews)
            {
                interviewsDto.Add(new InterviewStatusDTO()
                {
                    Id = interview.Id,
                    Status = interview.Status,
                    IsActive = interview.IsActive,
                    CreatedBy = interview.CreatedBy,
                    CreatedDate = interview.CreatedDate,
                    UpdatedBy = interview.UpdatedBy,
                    UpdatedDate = interview.UpdatedDate
                });
            }
            return interviewsDto;
        }

        public async Task<InterviewStatusDTO> Get(string id)
        {
            var interviews = await _context.TblInterviewStatus.FirstOrDefaultAsync(t => t.Id == id); ;
            if (interviews == null) return null;

            return new InterviewStatusDTO
            {
                Id = interviews.Id,
                Status = interviews.Status,
                IsActive = interviews.IsActive,
                CreatedBy = interviews.CreatedBy,
                CreatedDate = interviews.CreatedDate,
                UpdatedBy = interviews.UpdatedBy,
                UpdatedDate = interviews.UpdatedDate
            };
        }

        public async Task<InterviewStatusDTO> Add(InterviewStatusDTO _object)
        {
            var interviewStatus = new InterviewStatus
            {
                Status = _object.Status,
                IsActive = _object.IsActive,
                CreatedBy = _object.CreatedBy,
                CreatedDate = _object.CreatedDate,
                UpdatedBy = _object.UpdatedBy,
                UpdatedDate = _object.UpdatedDate
            };

            _context.TblInterviewStatus.Add(interviewStatus);
            await _context.SaveChangesAsync();

            _object.Id = interviewStatus.Id;
            return _object;
        }

        public async Task<InterviewStatusDTO> Update(InterviewStatusDTO _object)
        {
            var interviewStatus = await _context.TblInterviewStatus.FindAsync(_object.Id);

            if (interviewStatus == null)
                throw new KeyNotFoundException("InterviewStatus not found");

            interviewStatus.Status = _object.Status;
            interviewStatus.IsActive = _object.IsActive;
            interviewStatus.CreatedBy = _object.CreatedBy;
            interviewStatus.CreatedDate = _object.CreatedDate;
            interviewStatus.UpdatedBy = _object.UpdatedBy;
            interviewStatus.UpdatedDate = _object.UpdatedDate;

            _context.Entry(interviewStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return _object;

        }


        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }

    }
}