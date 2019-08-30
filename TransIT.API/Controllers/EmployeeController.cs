using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factories;
using TransIT.BLL.Services.Interfaces;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ADMIN,ENGINEER")]
    public class EmployeeController : FilterController<EmployeeDTO>
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _employeeService = serviceFactory.EmployeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            return Json(await _employeeService.GetRangeAsync(offset, amount));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Json(await _employeeService.GetAsync(id));
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            return Json(await _employeeService.SearchAsync(search));
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] EmployeeDTO employeeDTO)
        {
            return CreatedAtAction(nameof(Create), await _employeeService.CreateAsync(employeeDTO));
        }

        [HttpGet("boardnumber/{number}")]
        public async Task<IActionResult> GetByBoardNumber(int number)
        {
            return Json(await _employeeService.GetByBoardNumberAsync(number));
        }

        [HttpGet("boardnumbers")]
        public async Task<IActionResult> GetBoardNumbers()
        {
            return Json(await _employeeService.GetBoardNumbersAsync());
        }

        [HttpGet("attach/users")]
        public async Task<IActionResult> GetNotAttachedUsers()
        {
            return Json(await _employeeService.GetNotAttachedUsersAsync());
        }

        [HttpGet("attach/{userId}")]
        public async Task<IActionResult> GetEmployeeForUserAsync([FromRoute] string userId)
        {
            return Json(await _employeeService.GetEmployeeForUserAsync(userId));
        }

        [HttpPost("attach/{employee}/{user}")]
        public async Task<IActionResult> AttachUserToEmployee([FromRoute] int employee, [FromRoute] string user)
        {
            var attachResult = await _employeeService.AttachUserAsync(employee, user);
            if (attachResult == null)
            {
                return BadRequest("Cannot attach user or user doesn't exist");
            }
            else
            {
                return Json(attachResult);
            }
        }

        [HttpDelete("attach/{employee}")]
        public async Task<IActionResult> RemoveUserFromEmployee([FromRoute] int employee)
        {
            var deleteResult = await _employeeService.RemoveUserAsync(employee);
            if (deleteResult != null)
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Employee doesn't exist");
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] EmployeeDTO employeeDTO)
        {
            employeeDTO.Id = id;
            return Json(await _employeeService.UpdateAsync(employeeDTO));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _employeeService.DeleteAsync(id);
            return NoContent();
        }
    }
}