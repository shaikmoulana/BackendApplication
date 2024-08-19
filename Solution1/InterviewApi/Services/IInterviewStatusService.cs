using DataServices.Models;

namespace InterviewApi.Services
{
    public interface IInterviewStatusService
    {
        Task<IEnumerable<InterviewStatus>> GetAll();
        Task<InterviewStatus> Get(string id);
        Task<InterviewStatus> Create(InterviewStatus interviewStatus);
        Task<InterviewStatus> Update(InterviewStatus interviewStatus);
        Task<bool> Delete(string id);
    }
}
