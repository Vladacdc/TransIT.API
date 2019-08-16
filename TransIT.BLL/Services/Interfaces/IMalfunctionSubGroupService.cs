using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Malfunction Subgroup model CRUD
    /// </summary>
    public interface IMalfunctionSubgroupService : ICrudService<MalfunctionSubgroupDTO>
    {
        Task<IEnumerable<MalfunctionSubgroupDTO>> GetByGroupNameAsync(string groupName);
    }
}
