using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDTO>();
            CreateMap<PostDTO, Post>()
                .ForMember(t => t.ModifiedById, opt => opt.Ignore())
                .ForMember(t => t.CreatedById, opt => opt.Ignore())
                .ForMember(t => t.Mod, opt => opt.Ignore())
                .ForMember(t => t.Create, opt => opt.Ignore())
                .ForMember(t => t.ModDate, opt => opt.Ignore())
                .ForMember(t => t.CreateDate, opt => opt.Ignore());
        }
    }
}
