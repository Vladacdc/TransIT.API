using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class BillProfile : Profile
    {
        public BillProfile()
        {
            CreateMap<BillDTO, Bill>()
                .ForMember(b => b.ModId, opt => opt.Ignore())
                .ForMember(b => b.CreateId, opt => opt.Ignore())
                .ForMember(b => b.Mod, opt => opt.Ignore())
                .ForMember(b => b.Create, opt => opt.Ignore())
                .ForMember(b => b.DocumentId, opt => opt.MapFrom(x => x.Document.Id))
                .ForMember(b => b.Document, opt => opt.Ignore())
                .ForMember(b => b.IssueId, opt => opt.MapFrom(x => x.Issue.Id))
                .ForMember(b => b.Issue, opt => opt.Ignore());
            CreateMap<Bill, BillDTO>();
        }
    }
}
