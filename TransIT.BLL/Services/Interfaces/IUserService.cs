using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// User model CRUD
    /// </summary>
    public interface IUserService : ICrudService<User>
    {
        Task<User> UpdatePasswordAsync(User user, string newPassword);
        Task<IEnumerable<User>> GetAssignees(uint offset, uint amount);
    }
}
