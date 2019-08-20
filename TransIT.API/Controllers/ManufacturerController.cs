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
    [Authorize(Roles = "ADMIN,ENGINEER,ANALYST")]
    public class ManufacturerController : FilterController<ManufacturerDTO>
    {
        private readonly IManufacturerService _manufacturerService;

        public ManufacturerController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _manufacturerService = serviceFactory.ManufacturerService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            return Json(await _manufacturerService.GetRangeAsync(offset, amount));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Json(await _manufacturerService.GetAsync(id));
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            return Json(await _manufacturerService.SearchAsync(search));
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] ManufacturerDTO manufacturerDto)
        {
            return CreatedAtAction(nameof(Create), await _manufacturerService.CreateAsync(manufacturerDto));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] ManufacturerDTO manufacturerDto)
        {
            manufacturerDto.Id = id;
            return Json(await _manufacturerService.UpdateAsync(manufacturerDto));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _manufacturerService.DeleteAsync(id);
            return NoContent();
        }
    }
}