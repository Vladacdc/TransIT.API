using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Issue type model CRUD
    /// </summary>
    public interface IIssueService : ICrudService<int, Issue>
    {
        /// <summary>
        /// Gets issues specific for current customer
        /// </summary>
        /// <param name="userId">Id of customer</param>
        /// <returns>List of issues</returns>
        Task<IEnumerable<Issue>> GetRegisteredIssuesAsync(uint offset, uint amount, string userId);

        /// <summary>
        /// Remove issue if current user owns it.
        /// </summary>
        /// <param name="issueId">Id of issue to delete</param>
        /// <param name="userId">Id of user</param>
        /// <returns>void</returns>
        Task DeleteByUserAsync(int issueId, string userId);
    }
}
