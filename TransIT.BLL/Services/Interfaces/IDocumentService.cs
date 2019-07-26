using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Document type model CRUD
    /// </summary>
    public interface IDocumentService : ICrudService<DocumentDTO>
    {
        Task<IEnumerable<DocumentDTO>> GetRangeByIssueLogIdAsync(int issueLogId);

        Task<DocumentDTO> GetDocumentWithData(int documentId);

        Task<DocumentDTO> CreateAsync(DocumentDTO documentDTO, int? userId);
    }
}
