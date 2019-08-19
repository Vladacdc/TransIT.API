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
                .ForMember(a => a.UpdatedById, opt => opt.Ignore())
                .ForMember(a => a.CreatedById, opt => opt.Ignore())
                .ForMember(a => a.UpdatedDate, opt => opt.Ignore())
                .ForMember(a => a.CreatedDate, opt => opt.Ignore());
            CreateMap<PartIn, PartInDTO>();
        }
    }
}
