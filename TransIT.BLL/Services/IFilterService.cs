using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services
{    
    public interface IFilterService<TEntityDTO> where TEntityDTO : class, new()
    {
        Task<ulong> TotalRecordsAmountAsync();
        Task<IEnumerable<TEntityDTO>> GetQueriedAsync(DataTableRequestDTO dataFilter);
    }
}
