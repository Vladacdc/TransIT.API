using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryDTO, Country>()
                .ForMember(m => m.Supplier, opt => opt.Ignore())
                .ForMember(m => m.ModId, opt => opt.Ignore())
                .ForMember(m => m.CreateId, opt => opt.Ignore());
            CreateMap<Country, CountryDTO>();
        }
    }
}
