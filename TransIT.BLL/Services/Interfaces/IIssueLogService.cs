using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// IssueLog type model CRUD
    /// </summary>
    public interface IIssueLogService : ICrudService<IssueLogDTO>
    {
        /// <summary>
        /// Find issue logs with related issue
        /// </summary>
        /// <param name="issueId"></param>
        /// <returns>Related issue log to issue</returns>
        Task<IEnumerable<IssueLogDTO>> GetRangeByIssueIdAsync(int issueId);
    }
}
