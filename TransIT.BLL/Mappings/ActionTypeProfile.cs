using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class ActionTypeProfile : Profile
    {
        public ActionTypeProfile()
        {
            CreateMap<ActionTypeDTO, ActionType>()
                .ForMember(a => a.UpdatedById, opt => opt.Ignore())
                .ForMember(a => a.CreatedById, opt => opt.Ignore())
                .ForMember(a => a.Mod, opt => opt.Ignore())
                .ForMember(a => a.Create, opt => opt.Ignore())
                .ForMember(a => a.UpdatedDate, opt => opt.Ignore())
                .ForMember(a => a.CreatedDate, opt => opt.Ignore())
                .ForMember(a => a.IssueLog, opt => opt.Ignore());
            CreateMap<ActionType, ActionTypeDTO>();
        }
    }
}
