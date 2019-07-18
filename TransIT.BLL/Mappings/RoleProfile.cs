using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleDTO>();
            CreateMap<RoleDTO, Role>()
                .ForMember(t => t.ModifiedById, opt => opt.Ignore())
                .ForMember(t => t.CreatedById, opt => opt.Ignore())
                .ForMember(t => t.Mod, opt => opt.Ignore())
                .ForMember(t => t.Create, opt => opt.Ignore())
                .ForMember(t => t.ModDate, opt => opt.Ignore())
                .ForMember(t => t.CreateDate, opt => opt.Ignore())
                .ForMember(t => t.User, opt => opt.Ignore());
        }
    }
}
