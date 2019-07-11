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
                .ForMember(t => t.ModId, opt => opt.Ignore())
                .ForMember(t => t.CreateId, opt => opt.Ignore())
                .ForMember(t => t.Mod, opt => opt.Ignore())
                .ForMember(t => t.Create, opt => opt.Ignore())
                .ForMember(t => t.ModDate, opt => opt.Ignore())
                .ForMember(t => t.CreateDate, opt => opt.Ignore())
                .ForMember(t => t.RefreshToken, opt => opt.Ignore());
            CreateMap<Token, TokenDTO>()
                .ForMember(t => t.AccessToken, opt => opt.Ignore());
        }        
    }
}
