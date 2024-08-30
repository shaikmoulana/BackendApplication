using DataServices.Models;

namespace InterviewApi.Services
{
    public interface IInterviewStatusService
    {
        Task<IEnumerable<InterviewStatusDTO>> GetAll();
        Task<InterviewStatusDTO> Get(string id);
        Task<InterviewStatusDTO> Add(InterviewStatusDTO interviewStatus);
        Task<InterviewStatusDTO> Update(InterviewStatusDTO interviewStatus);
        Task<bool> Delete(string id);
    }
}
