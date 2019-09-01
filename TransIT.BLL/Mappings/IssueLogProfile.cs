using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class IssueLogProfile : Profile
    {
        public IssueLogProfile()
        {
            CreateMap<IssueLogDTO, IssueLog>()
                .ForMember(i => i.Document, opt => opt.Ignore())
                .ForMember(i => i.UpdatedById, opt => opt.Ignore())
                .ForMember(i => i.CreatedById, opt => opt.Ignore())
                .ForMember(i => i.Mod, opt => opt.Ignore())
                .ForMember(i => i.Create, opt => opt.Ignore())
                .ForMember(i => i.Document, opt => opt.Ignore())
                .ForMember(i => i.IssueId, opt => opt.MapFrom(x => x.Issue.Id))
                .ForMember(i => i.SupplierId, opt => opt.Condition((dto, model) => dto.Supplier != null))
                .ForMember(i => i.SupplierId, opt => opt.MapFrom(x => x.Supplier.Id))
                .ForMember(i => i.Supplier, opt => opt.Ignore())
                .ForMember(i => i.NewStateId, opt => opt.MapFrom(x => x.NewState.Id))
                .ForMember(i => i.OldStateId, opt => opt.Ignore())
                .ForMember(i => i.ActionTypeId, opt => opt.MapFrom(x => x.ActionType.Id))
                .ForMember(i => i.NewState, opt => opt.Ignore())
                .ForMember(i => i.OldState, opt => opt.Ignore())
                .ForMember(i => i.ActionType, opt => opt.Ignore())
                .ForMember(i => i.WorkType, opt => opt.Ignore())
                .ForMember(i => i.WorkTypeId, opt => opt.MapFrom(x => x.WorkType.Id))
                .ForMember(v => v.UpdatedDate, opt => opt.Ignore())
                .ForMember(v => v.CreatedDate, opt => opt.Ignore());

            CreateMap<IssueLog, IssueLogDTO>()
                .ForMember(i => i.Documents, opt => opt.MapFrom(x => x.Document))
                .PreserveReferences();
        }
    }
}
