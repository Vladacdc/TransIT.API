using System.Linq;
using System.Linq.Expressions;
using System.Net.Mime;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.BLL.Services.ImplementedServices;

namespace TransIT.BLL.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {

            CreateMap<UserDTO, User>()                
                .ForMember(u => u.UpdatedById, opt => opt.Ignore())
                .ForMember(u => u.CreatedById, opt => opt.Ignore())
                .ForMember(u => u.ModifiedBy, opt => opt.Ignore())
                .ForMember(u => u.CreatedBy, opt => opt.Ignore())
                .ForMember(u => u.UpdatedDate, opt => opt.Ignore())
                .ForMember(u => u.CreatedDate, opt => opt.Ignore())

                .ForMember(u => u.BillMod, opt => opt.Ignore())
                .ForMember(u => u.MalfunctionGroupMod, opt => opt.Ignore())
                .ForMember(u => u.MalfunctionSubgroupMod, opt => opt.Ignore())
                .ForMember(u => u.MalfunctionMod, opt => opt.Ignore())
                .ForMember(u => u.IssueMod, opt => opt.Ignore())
                .ForMember(u => u.IssueLogMod, opt => opt.Ignore())
                .ForMember(u => u.DocumentMod, opt => opt.Ignore())
                .ForMember(u => u.VehicleMod, opt => opt.Ignore())
                .ForMember(u => u.VehicleTypeMod, opt => opt.Ignore())
                .ForMember(u => u.ActionTypeMod, opt => opt.Ignore())
                .ForMember(u => u.InverseMod, opt => opt.Ignore())
                .ForMember(u => u.SupplierMod, opt => opt.Ignore())

                .ForMember(u => u.BillCreate, opt => opt.Ignore())
                .ForMember(u => u.MalfunctionGroupCreate, opt => opt.Ignore())
                .ForMember(u => u.MalfunctionSubgroupCreate, opt => opt.Ignore())
                .ForMember(u => u.MalfunctionCreate, opt => opt.Ignore())
                .ForMember(u => u.IssueCreate, opt => opt.Ignore())
                .ForMember(u => u.IssueLogCreate, opt => opt.Ignore())
                .ForMember(u => u.DocumentCreate, opt => opt.Ignore())
                .ForMember(u => u.VehicleCreate, opt => opt.Ignore())
                .ForMember(u => u.VehicleTypeCreate, opt => opt.Ignore())
                .ForMember(u => u.ActionTypeCreate, opt => opt.Ignore())
                .ForMember(u => u.InverseCreate, opt => opt.Ignore())
                .ForMember(u => u.SupplierCreate, opt => opt.Ignore());
            CreateMap<User, UserDTO>()
                .ForMember(u => u.Password, opt => opt.Ignore())
                .ForMember(u => u.Role, opt => opt.MapFrom(x => UserService.GetRoleAsync(x).Result));
        }

    }
}
