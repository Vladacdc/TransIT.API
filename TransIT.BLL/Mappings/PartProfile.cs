using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Mappings
{
    public class PartProfile : Profile
    {
        public PartProfile()
        {
            CreateMap<PartDTO, Part>().ReverseMap();
        }
    }
}
