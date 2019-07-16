using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<CurrencyDTO, Currency>()
                .ForMember(m => m.Supplier, opt => opt.Ignore())
                .ForMember(m => m.ModId, opt => opt.Ignore())
                .ForMember(m => m.CreateId, opt => opt.Ignore());
            CreateMap<Currency, CurrencyDTO>();
        }
    }
}
