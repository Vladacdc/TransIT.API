using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Employee model CRUD
    /// </summary>
    public interface IEmployeeService : ICrudService<EmployeeDTO>
    {
        Task<EmployeeDTO> CreateAsync(int userId, EmployeeDTO dto);

        Task<EmployeeDTO> UpdateAsync(int userId, EmployeeDTO dto);
    }
}
