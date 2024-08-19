using DataServices.Models;

namespace WebinarsApi.Services
{
    public interface IWebinarService
    {
        public Task<IEnumerable<Webinars>> GetAll();
        public Task<Webinars> Get(string id);
        public Task<Webinars> Add(Webinars _object);
        public Task<Webinars> Update(Webinars _object);
        public Task<bool> Delete(string id);
    }
}
