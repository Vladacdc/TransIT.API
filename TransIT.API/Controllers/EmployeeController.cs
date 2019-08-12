using System;
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
            try
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
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
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
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            try
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
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] EmployeeDTO employeeDTO)
        {
            try
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
            catch (Exception e)
            {
                throw e;
            }
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
            IActionResult result;
            if (attachResult == EmployeeDTO.CannotAttachUser)
            {
                result = BadRequest("Cannot attach this user, because it's already attached.");
            }
            else if (attachResult == EmployeeDTO.DoesNotExist)
            {
                result = BadRequest("User or employee doesn't exist.");
            }
            else
            {
                result = Ok(attachResult);
            }
            return result;
        }

        [HttpDelete("attach/{employee}")]
        public async Task<IActionResult> RemoveUserFromEmployee([FromRoute] int employee)
        {
            var deleteResult = await _employeeService.RemoveUserAsync(employee);
            if (deleteResult != EmployeeDTO.DoesNotExist)
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
            try
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
            catch (Exception e)
            {
                throw e;
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