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
    [Authorize(Roles = "ADMIN,ENGINEER,REGISTER,ANALYST")]
    public class SupplierController : FilterController<SupplierDTO>
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _supplierService = serviceFactory.SupplierService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            return Json(await _supplierService.GetRangeAsync(offset, amount));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Json(await _supplierService.GetAsync(id));
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            return Json(await _supplierService.SearchAsync(search));
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] SupplierDTO supplierDto)
        {
            return CreatedAtAction(nameof(Create), await _supplierService.CreateAsync(supplierDto));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierDTO supplierDto)
        {
            supplierDto.Id = id;
            return Json(await _supplierService.UpdateAsync(supplierDto));
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