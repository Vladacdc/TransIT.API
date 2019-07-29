using System.Threading.Tasks;
using System.Collections.Generic;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services
{    
    public interface IFilterService<TEntityDTO> where TEntityDTO : class, new()
    {
        Task<ulong> TotalRecordsAmountAsync();
        Task<IEnumerable<TEntityDTO>> GetQueriedAsync(DataTableRequestDTO dataFilter);
    }
}
