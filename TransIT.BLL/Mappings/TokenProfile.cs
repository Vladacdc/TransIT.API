using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<TokenDTO, Token>()
                .ForMember(t => t.UpdatedById, opt => opt.Ignore())
                .ForMember(t => t.CreatedById, opt => opt.Ignore())
                .ForMember(t => t.Mod, opt => opt.Ignore())
                .ForMember(t => t.Create, opt => opt.Ignore())
                .ForMember(t => t.UpdatedDate, opt => opt.Ignore())
                .ForMember(t => t.CreatedDate, opt => opt.Ignore())
                .ForMember(t => t.RefreshToken, opt => opt.Ignore());
            CreateMap<Token, TokenDTO>()
                .ForMember(t => t.AccessToken, opt => opt.Ignore());
        }        
    }
}
