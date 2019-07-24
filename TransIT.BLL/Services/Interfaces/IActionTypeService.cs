using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Action type model CRUD
    /// </summary>
    public interface IActionTypeService : ICrudService<ActionTypeDTO>
    {
        Task<ActionTypeDTO> CreateAsync(int userId, ActionTypeDTO model);

        Task<ActionTypeDTO> UpdateAsync(int userId, ActionTypeDTO model);
    }
}
