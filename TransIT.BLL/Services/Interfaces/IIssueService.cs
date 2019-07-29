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
        Task<ulong> GetTotalRecordsForSpecificUser(string userId);

        /// <summary>
        /// Gets all issues specific for current user.
        /// </summary>
        /// <param name="user">A id of user, which created given issues.</param>
        /// <returns>List of issues, which matched this query.</returns>
        Task<IEnumerable<IssueDTO>> GetIssuesBySpecificUser(string user);

        /// <summary>
        /// Gets issues specific for current customer
        /// </summary>
        /// <param name="user">Id of customer</param>
        /// <returns>List of issues</returns>
        Task<IEnumerable<IssueDTO>> GetRegisteredIssuesAsync(uint offset, uint amount, string user);
    }
}
