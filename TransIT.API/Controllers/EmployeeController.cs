using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using System.Security.Claims;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
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
                return BadRequest();
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
                return BadRequest();
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
                return BadRequest();
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
                return BadRequest();
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
                return BadRequest();
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
