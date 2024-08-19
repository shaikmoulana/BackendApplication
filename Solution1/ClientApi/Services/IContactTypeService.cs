using DataServices.Models;

namespace ClientServices.Services
{
    public interface IContactTypeService
    {
        public Task<IEnumerable<ContactType>> GetAll();
        public Task<ContactType> Get(string id);
        public Task<ContactType> Add(ContactType contactType);
        public Task<ContactType> Update(ContactType contactType);
        public Task<bool> Delete(string id);
    }
   
}


