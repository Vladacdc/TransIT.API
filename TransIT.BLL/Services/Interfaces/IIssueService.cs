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
        /// Gets a total number of issues, created by a current logged in system user. 
        /// </summary>
        /// <returns>An integer number.</returns>
        Task<ulong> GetTotalRecordsForCurrentUser();

        /// <summary>
        /// Gets all issues specific for current logged in system user.
        /// </summary>
        /// <returns>List of issues, which matched this query.</returns>
        Task<IEnumerable<IssueDTO>> GetIssuesByCurrentUser();

        /// <summary>
        /// Gets issues specific for current customer
        /// </summary>
        /// <returns>List of issues</returns>
        Task<IEnumerable<IssueDTO>> GetRegisteredIssuesAsync(uint offset, uint amount);
    }
}
