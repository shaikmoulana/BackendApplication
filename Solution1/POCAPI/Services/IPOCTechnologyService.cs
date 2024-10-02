using DataServices.Models;

namespace POCAPI.Services
{
    public interface IPOCTechnologyService
    {
        Task<IEnumerable<POCTechnologyDTO>> GetAll();
        Task<POCTechnologyDTO> Get(string id);
        Task<POCTechnologyDTO> Add(POCTechnologyDTO _object);
        Task<POCTechnologyDTO> Update(POCTechnologyDTO _object);
        Task<bool> Delete(string id);
    }
}
