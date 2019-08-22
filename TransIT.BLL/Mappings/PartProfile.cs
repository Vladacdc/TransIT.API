using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class PartProfile : Profile
    {
        public PartProfile()
        {
<<<<<<< HEAD
            CreateMap<PartDTO, Part>().ReverseMap();
=======
            CreateMap<PartDTO, Part>()
                .ForMember(e => e.UnitId, opt => opt.MapFrom(e => e.Unit.Id))
                .ForMember(e => e.ManufacturerId, opt => opt.MapFrom(e => e.Manufacturer.Id))
                .ForMember(e => e.Unit, opt => opt.Ignore())
                .ForMember(e => e.Manufacturer, opt => opt.Ignore());

            CreateMap<Part, PartDTO>();
>>>>>>> eb66ff9af4c4102c1e2251cc22beb91b11c7fdc2
        }
    }
}
