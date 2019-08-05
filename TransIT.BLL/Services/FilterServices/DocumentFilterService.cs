using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class DocumentFilterService : BaseFilterService<Document, DocumentDTO>
    {
        public DocumentFilterService(IQueryRepository<Document> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
