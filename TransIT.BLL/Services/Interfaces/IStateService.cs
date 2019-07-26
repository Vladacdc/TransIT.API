using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Action type model CRUD
    /// </summary>
    public interface IStateService : ICrudService<StateDTO>
    {
        /// <summary>
        /// Gets state by name
        /// </summary>
        /// <param name="name">State's name</param>
        /// <returns>State</returns>
        Task<StateDTO> GetStateByNameAsync(string name);
    }
}
