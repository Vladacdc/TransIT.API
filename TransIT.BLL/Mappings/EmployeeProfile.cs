using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<EmployeeDTO, Employee>()
                .ForMember(t => t.UpdatedById, opt => opt.Ignore())
                .ForMember(t => t.CreatedById, opt => opt.Ignore())
                .ForMember(t => t.Mod, opt => opt.Ignore())
                .ForMember(t => t.Create, opt => opt.Ignore())
                .ForMember(t => t.UpdatedDate, opt => opt.Ignore())
                .ForMember(t => t.CreatedDate, opt => opt.Ignore())
                .ForMember(t => t.Post, opt => opt.Ignore())
                .ForMember(t => t.PostId, opt => opt.MapFrom(d => d.Post.Id));
        }
    }
}
