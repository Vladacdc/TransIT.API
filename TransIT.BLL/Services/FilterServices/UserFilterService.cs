using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class UserFilterService : BaseFilterService<User, UserDTO>
    {
        public UserFilterService(IQueryRepository<User> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
