using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// IssueLog type model CRUD
    /// </summary>
    public interface IIssueLogService : ICrudService<IssueLog>
    {
        /// <summary>
        /// Find issue logs with related issue
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns>Related issue log to issue</returns>
        Task<IEnumerable<IssueLog>> GetRangeByIssueIdAsync(int issueId);
    }
}
