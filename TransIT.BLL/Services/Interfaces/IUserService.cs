using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// User model CRUD
    /// </summary>
    public interface IUserService
    {
        Task<UserDTO> UpdatePasswordAsync(UserDTO user, string oldPassword, string newPassword);
        Task<IEnumerable<UserDTO>> GetAssignees(uint offset, uint amount);
        Task<UserDTO> GetAsync(string id);
        Task<IEnumerable<UserDTO>> GetRangeAsync(uint offset, uint amount);
        Task<UserDTO> CreateAsync(UserDTO value);
        Task<UserDTO> UpdateAsync(UserDTO value);
        Task DeleteAsync(string id);
        Task<IEnumerable<RoleDTO>> GetRoles();
    }
}
