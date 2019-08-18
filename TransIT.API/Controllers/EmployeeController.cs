using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory, ILogger<EmployeeController> logger)
            : base(filterServiceFactory)
        {
            _employeeService = serviceFactory.EmployeeService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _employeeService.GetRangeAsync(offset, amount);
            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return null;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _employeeService.GetAsync(id);
            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return null;
            }
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _employeeService.SearchAsync(search);
            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return null;
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] EmployeeDTO employeeDTO)
        {
            var createdDTO = await _employeeService.CreateAsync(employeeDTO);

            if (createdDTO != null)
            {
                return CreatedAtAction(nameof(Create), createdDTO);
            }
            else
            {
                return null;
            }
        }

        [HttpGet("boardnumber/{number}")]
        public async Task<IActionResult> GetByBoardNumber(int number)
        {
            return Ok(await _employeeService.GetByBoardNumberAsync(number));
        }

        [HttpGet("boardnumbers")]
        public async Task<IActionResult> GetBoardNumbers()
        {
            return Ok(await _employeeService.GetBoardNumbersAsync());
        }

        [HttpGet("attach/users")]
        public async Task<IActionResult> GetNotAttachedUsers()
        {
            return Ok(await _employeeService.GetNotAttachedUsersAsync());
        }

        [HttpGet("attach/{userId}")]
        public async Task<IActionResult> GetEmployeeForUserAsync([FromRoute] string userId)
        {
            return Ok(await _employeeService.GetEmployeeForUserAsync(userId));
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
                return Ok(attachResult);
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

            var result = await _employeeService.UpdateAsync(employeeDTO);

            if (result != null)
            {
                return NoContent();
            }
            else
            {
                return null;
            }
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