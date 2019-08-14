using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    /// <summary>
    /// Employee model CRUD
    /// </summary>
    public interface IEmployeeService : ICrudService<EmployeeDTO>
    {
        /// <summary>
        /// Gets a list of users, which were not attached to
        /// an employee, though can be attached.
        /// </summary>
        /// <returns>A list of <see cref="UserDTO"/>.</returns>
        Task<List<UserDTO>> GetNotAttachedUsersAsync();

        /// <summary>
        /// Gets an employee, which has a given user attached to it.
        /// </summary>
        /// <param name="user">A user ID.</param>
        /// <returns>A data tranfer object, with attached user.</returns>
        Task<EmployeeDTO> GetEmployeeForUserAsync(string user);

        /// <summary>
        /// Attaches an existing user to employee.
        /// </summary>
        /// <param name="employee">An employee ID.</param>
        /// <param name="user">A user ID.</param>
        /// <returns>A data tranfer object, with attached user.</returns>
        Task<EmployeeDTO> AttachUserAsync(int employee, string user);

        /// <summary>
        /// Removes an attached user from employee account.
        /// </summary>
        /// <param name="employee">An employee ID.</param>
        /// <returns>A data tranfer object, without attached user.</returns>
        Task<EmployeeDTO> RemoveUserAsync(int employee);

        /// <summary>
        /// Gets all board numbers at once.
        /// </summary>
        /// <returns>A list of integers.</returns>
        Task<List<int>> GetBoardNumbers();

        /// <summary>
        /// Gets an employee by its board number.
        /// </summary>
        /// <param name="boardNumber">A board number.</param>
        /// <returns>An employee dto.</returns>
        Task<EmployeeDTO> GetByBoardNumber(int boardNumber);
    }
}
