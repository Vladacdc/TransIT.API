using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class MalfunctionGroupProfile : Profile
    {
        public MalfunctionGroupProfile()
        {
            CreateMap<MalfunctionGroupDTO, MalfunctionGroup>()
                .ForMember(m => m.ModId, opt => opt.Ignore())
                .ForMember(m => m.CreateId, opt => opt.Ignore())
                .ForMember(m => m.Mod, opt => opt.Ignore())
                .ForMember(m => m.Create, opt => opt.Ignore())
                .ForMember(m => m.ModDate, opt => opt.Ignore())
                .ForMember(m => m.CreateDate, opt => opt.Ignore());
            CreateMap<MalfunctionGroup, MalfunctionGroupDTO>();
        }
    }
}
