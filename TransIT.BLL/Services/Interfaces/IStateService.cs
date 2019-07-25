using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Action type model CRUD
    /// </summary>
    public interface IStateService : ICrudService<State>
    {
        /// <summary>
        /// Gets state by name
        /// </summary>
        /// <param name="name">State's name</param>
        /// <returns>State</returns>
        Task<State> GetStateByNameAsync(string name);
    }
}
