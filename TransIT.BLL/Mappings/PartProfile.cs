using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class PartProfile : Profile
    {
        public PartProfile()
        {
            CreateMap<PartDTO, Part>()
                .ForMember(e => e.UpdatedById, opt => opt.Ignore())
                .ForMember(e => e.CreatedById, opt => opt.Ignore())
                .ForMember(e => e.UpdatedDate, opt => opt.Ignore())
                .ForMember(e => e.CreatedDate, opt => opt.Ignore())
                .ForMember(e => e.UnitId, opt => opt.MapFrom(e => e.Unit.Id))
                .ForMember(e => e.ManufacturerId, opt => opt.MapFrom(e => e.Manufacturer.Id))
                .ForMember(e => e.Unit, opt => opt.Ignore())
                .ForMember(e => e.Manufacturer, opt => opt.Ignore());

            CreateMap<Part, PartDTO>();
        }
    }
}
