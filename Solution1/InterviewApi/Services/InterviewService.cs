using DataServices.Models;
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
            /*interview.SOWRequirement = interview.SOWRequirement;
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
            interview.UpdatedDate = interview.UpdatedDate;*/
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
