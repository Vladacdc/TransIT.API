using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class VehicleTypeProfile : Profile
    {
        public VehicleTypeProfile()
        {
            CreateMap<VehicleTypeDTO, VehicleType>()
                .ForMember(v => v.ModifiedById, opt => opt.Ignore())
                .ForMember(v => v.CreatedById, opt => opt.Ignore())
                .ForMember(v => v.Mod, opt => opt.Ignore())
                .ForMember(v => v.Create, opt => opt.Ignore())
                .ForMember(v => v.Vehicle, opt => opt.Ignore())
                .ForMember(v => v.ModDate, opt => opt.Ignore())
                .ForMember(v => v.CreateDate, opt => opt.Ignore());
            CreateMap<VehicleType, VehicleTypeDTO>();
        }
    }
}
