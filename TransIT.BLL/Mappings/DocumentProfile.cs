using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class DocumentProfile : Profile
    {
        public DocumentProfile()
        {
            CreateMap<DocumentDTO, Document>()
                .ForMember(d => d.UpdatedById, opt => opt.Ignore())
                .ForMember(d => d.CreatedById, opt => opt.Ignore())
                .ForMember(d => d.Mod, opt => opt.Ignore())
                .ForMember(d => d.Create, opt => opt.Ignore())
                .ForMember(d => d.Bill, opt => opt.Ignore())
                .ForMember(d => d.IssueLogId, opt => opt.Condition((dto, model) => dto.IssueLog != null))
                .ForMember(d => d.IssueLogId, opt => opt.MapFrom(x => x.IssueLog.Id))
                .ForMember(d => d.IssueLog, opt => opt.Ignore());

            CreateMap<Document, DocumentDTO>();
        }
    }
}
