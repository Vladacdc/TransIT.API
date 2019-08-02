using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class PostFilterService : BaseFilterService<Post, PostDTO>
    {
        public PostFilterService(IQueryRepository<Post> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
