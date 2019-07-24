using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Bill type model CRUD
    /// </summary>
    public interface IBillService : ICrudService<BillDTO>
    {
        Task<BillDTO> CreateAsync(int userId, BillDTO model);

        Task<BillDTO> UpdateAsync(int userId, BillDTO model);
    }
}
