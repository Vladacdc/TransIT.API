using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class PartInProfile : Profile
    {
        public PartInProfile()
        {
            CreateMap<PartInDTO, PartIn>()
                .ForMember(e => e.UpdatedById, opt => opt.Ignore())
                .ForMember(e => e.CreatedById, opt => opt.Ignore())
                .ForMember(e => e.UpdatedDate, opt => opt.Ignore())
                .ForMember(e => e.CreatedDate, opt => opt.Ignore());

            CreateMap<PartIn, PartInDTO>()
                .ForMember(e => e.UnitId, opt => opt.Ignore())
                .ForMember(e => e.PartId, opt => opt.Ignore())
                .ForMember(e => e.CurrencyId, opt => opt.Ignore());
        }
    }
}
