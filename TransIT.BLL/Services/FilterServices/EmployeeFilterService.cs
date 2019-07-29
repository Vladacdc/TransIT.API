using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;

namespace TransIT.BLL.Services.FilterServices
{
    public class EmployeeFilterService : BaseFilterService<Employee, EmployeeDTO>
    {
        public EmployeeFilterService(IQueryRepository<Employee> repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
