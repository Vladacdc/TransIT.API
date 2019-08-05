using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class MalfunctionProfile : Profile
    {
        public MalfunctionProfile()
        {
            CreateMap<MalfunctionDTO, Malfunction>()
                .ForMember(m => m.UpdatedById, opt => opt.Ignore())
                .ForMember(m => m.CreatedById, opt => opt.Ignore())
                .ForMember(m => m.Mod, opt => opt.Ignore())
                .ForMember(m => m.Create, opt => opt.Ignore())
                .ForMember(m => m.UpdatedDate, opt => opt.Ignore())
                .ForMember(m => m.CreatedDate, opt => opt.Ignore())
                .ForMember(m => m.MalfunctionSubgroupId, opt => opt.MapFrom(x => x.MalfunctionSubgroup.Id))
                .ForMember(m => m.MalfunctionSubgroup, opt => opt.Ignore());
            CreateMap<Malfunction, MalfunctionDTO>();
        }
    }
}
