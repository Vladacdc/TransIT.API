using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services
{    
    public interface IFilterService<TDTO> where TDTO : class, new()
    {
        ulong TotalRecordsAmount();
        Task<IEnumerable<TDTO>> GetQueriedAsync();
        Task<IEnumerable<TDTO>> GetQueriedAsync(DataTableRequestDTO dataFilter);
    }
}
