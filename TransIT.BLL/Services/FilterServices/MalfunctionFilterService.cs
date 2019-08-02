using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class MalfunctionFilterService : BaseFilterService<Malfunction, MalfunctionDTO>
    {
        public MalfunctionFilterService(IQueryRepository<Malfunction> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
