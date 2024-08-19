using DataServices.Models;

namespace InterviewApi.Services
{
    public interface IInterviewService
    {
        public Task<IEnumerable<Interviews>> GetAll();
        public Task<Interviews> Get(string id);
        public Task<Interviews> Create(Interviews interview);
        public Task<Interviews> Update(Interviews interview);
        public Task<bool> Delete(string id);
    }
}
