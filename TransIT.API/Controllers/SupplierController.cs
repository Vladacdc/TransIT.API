using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.Interfaces;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,REGISTER,ANALYST")]
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;
        
        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _supplierService.GetRangeAsync(offset, amount);
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
            var result = await _supplierService.GetAsync(id);
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
            var result = await _supplierService.SearchAsync(search);
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
        public async Task<IActionResult> Create([FromBody] SupplierDTO supplierDto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var createdDto = await _supplierService.CreateAsync(supplierDto);
            if (createdDto != null)
            {
                return CreatedAtAction(nameof(Create), createdDto);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierDTO supplierDto)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            supplierDto.Id = id;

            var result = await _supplierService.UpdateAsync(supplierDto);

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
            await _supplierService.DeleteAsync(id);
            return NoContent();
        }
    }
}
