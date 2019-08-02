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
                .ForMember(m => m.UpdatedById, opt => opt.Ignore())
                .ForMember(m => m.CreatedById, opt => opt.Ignore());
            CreateMap<Country, CountryDTO>();
        }
    }
}
