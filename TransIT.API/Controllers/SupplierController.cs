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
            var result = await _supplierService.GetRangeAsync(offset, amount);
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
            var result = await _supplierService.GetAsync(id);
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
            var result = await _supplierService.SearchAsync(search);
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
        public async Task<IActionResult> Create([FromBody] SupplierDTO supplierDto)
        {
            var createdDto = await _supplierService.CreateAsync(supplierDto);
            if (createdDto != null)
            {
                return CreatedAtAction(nameof(Create), createdDto);
            }
            else
            {
                return null;
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] SupplierDTO supplierDto)
        {
            supplierDto.Id = id;

            var result = await _supplierService.UpdateAsync(supplierDto);

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
            await _supplierService.DeleteAsync(id);
            return NoContent();
        }
    }
}