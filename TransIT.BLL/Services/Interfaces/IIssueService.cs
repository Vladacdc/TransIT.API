using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Issue type model CRUD
    /// </summary>
    public interface IIssueService : ICrudService<IssueDTO>
    {
        /// <summary>
        /// Gets issues specific for current customer
        /// </summary>
        /// <param name="userId">Id of customer</param>
        /// <returns>List of issues</returns>
        Task<IEnumerable<IssueDTO>> GetRegisteredIssuesAsync(uint offset, uint amount, string userId);
    }
}
