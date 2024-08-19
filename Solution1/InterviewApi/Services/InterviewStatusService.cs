using DataServices.Models;
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

