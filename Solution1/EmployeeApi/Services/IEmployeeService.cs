using DataServices.Models;

namespace EmployeeApi.Services
{
    public interface IEmployeeService
    {
            Task<IEnumerable<Employee>> GetAll();
            Task<Employee> Get(string id);
            Task<Employee> Add(Employee employee);
            Task<Employee> Update(Employee employee);
            Task<bool> Delete(string id);

     
    }
}
