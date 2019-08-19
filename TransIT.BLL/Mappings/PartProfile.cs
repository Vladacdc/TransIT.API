using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class PartProfile : Profile
    {
        public PartProfile()
        {
            //TODO

            CreateMap<PartDTO, Part>()
                .ForMember(b => b.UpdatedById, opt => opt.Ignore())
                .ForMember(b => b.CreatedById, opt => opt.Ignore())
                .ForMember(b => b.Mod, opt => opt.Ignore())
                .ForMember(b => b.Create, opt => opt.Ignore());
            CreateMap<Part, PartDTO>();
        }
    }
}
