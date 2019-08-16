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
                _logger.LogError(e, e.Message);
                return StatusCode(500, new ExtendedErrorDTO(e));
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
                _logger.LogError(e, e.Message);
                return StatusCode(500, new ExtendedErrorDTO(e));
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
                _logger.LogError(e, e.Message);
                return StatusCode(500, new ExtendedErrorDTO(e));
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
                _logger.LogError(e, e.Message);
                return StatusCode(500, new ExtendedErrorDTO(e));
            }
        }

        [HttpGet("boardnumber/{number}")]
        public async Task<IActionResult> GetByBoardNumber(int number)
        {
            try
            {
                return Ok(await _employeeService.GetByBoardNumberAsync(number));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, new ExtendedErrorDTO(e));
            }
        }

        [HttpGet("boardnumbers")]
        public async Task<IActionResult> GetBoardNumbers()
        {
            try
            {
                return Ok(await _employeeService.GetBoardNumbersAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, new ExtendedErrorDTO(e));
            }
        }

        [HttpGet("attach/users")]
        public async Task<IActionResult> GetNotAttachedUsers()
        {
            try
            {
                return Ok(await _employeeService.GetNotAttachedUsersAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, new ExtendedErrorDTO(e));
            }
        }

        [HttpGet("attach/{userId}")]
        public async Task<IActionResult> GetEmployeeForUserAsync([FromRoute] string userId)
        {
            try
            {
                return Ok(await _employeeService.GetEmployeeForUserAsync(userId));
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, new ExtendedErrorDTO(e));
            }
        }

        [HttpPost("attach/{employee}/{user}")]
        public async Task<IActionResult> AttachUserToEmployee([FromRoute] int employee, [FromRoute] string user)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, new ExtendedErrorDTO(e));
            }
        }

        [HttpDelete("attach/{employee}")]
        public async Task<IActionResult> RemoveUserFromEmployee([FromRoute] int employee)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, new ExtendedErrorDTO(e));
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
                _logger.LogError(e, e.Message);
                return StatusCode(500, new ExtendedErrorDTO(e));
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _employeeService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                return StatusCode(500, new ExtendedErrorDTO(e));
            }
        }
    }
}