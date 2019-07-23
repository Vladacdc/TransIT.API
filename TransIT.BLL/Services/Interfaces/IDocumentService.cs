using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Document type model CRUD
    /// </summary>
    public interface IDocumentService : ICrudService<int, Document>
    {
        Task<IEnumerable<Document>> GetRangeByIssueLogIdAsync(int issueLogId);
    }
}
