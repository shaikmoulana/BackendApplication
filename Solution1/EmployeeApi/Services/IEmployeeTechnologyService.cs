using DataServices.Models;

namespace EmployeeApi.Services
{
    public interface IEmployeeTechnologyService
    {
        Task<IEnumerable<EmployeeTechnology>> GetAll();
        Task<EmployeeTechnology> Get(string id);
        Task<EmployeeTechnology> Add(EmployeeTechnology employeeTechnology);
        Task<EmployeeTechnology> Update(EmployeeTechnology employeeTechnology);
        Task<bool> Delete(string id);
    }
}