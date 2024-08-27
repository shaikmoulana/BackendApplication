using DataServices.Models;

namespace EmployeeApi.Services
{
    public interface IEmployeeTechnologyService
    {
        Task<IEnumerable<EmployeeTechnologyDTO>> GetAll();
        Task<EmployeeTechnologyDTO> Get(string id);
        Task<EmployeeTechnologyDTO> Add(EmployeeTechnologyDTO employeeTechnology);
        Task<EmployeeTechnologyDTO> Update(EmployeeTechnologyDTO employeeTechnology);
        Task<bool> Delete(string id);
    }
}