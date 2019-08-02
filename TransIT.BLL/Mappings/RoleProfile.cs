using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, Role>()
                 .ForMember(t => t.ConcurrencyStamp, opt => opt.Ignore());
            CreateMap<Role, RoleDTO>()
                .ForMember(t => t.Name, opt => opt.MapFrom(t => t.Name));
            CreateMap<RoleDTO, Role>()
                .ForMember(t => t.UpdatedById, opt => opt.Ignore())
                .ForMember(t => t.CreatedById, opt => opt.Ignore())
                .ForMember(t => t.Mod, opt => opt.Ignore())
                .ForMember(t => t.Create, opt => opt.Ignore())
                .ForMember(t => t.UpdatedDate, opt => opt.Ignore())
                .ForMember(t => t.CreatedDate, opt => opt.Ignore())
                .ForMember(t => t.UserRoles, opt => opt.Ignore());
                
        }
    }
}
