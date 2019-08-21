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
                .ForMember(e => e.CreatedDate, opt => opt.Ignore())
                .ForMember(e => e.PartId, opt => opt.MapFrom(e => e.Part.Id))
                .ForMember(e => e.UnitId, opt => opt.MapFrom(e => e.Unit.Id))
                .ForMember(e => e.CurrencyId, opt => opt.MapFrom(e => e.Currency.Id))
                .ForMember(e => e.Currency, opt => opt.Ignore())
                .ForMember(e => e.Part, opt => opt.Ignore())
                .ForMember(e => e.Unit, opt => opt.Ignore());

            CreateMap<PartIn, PartInDTO>();
        }
    }
}
