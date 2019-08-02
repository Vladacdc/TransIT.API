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
    [Authorize(Roles = "ADMIN,ENGINEER,REGISTER")]
    public class VehicleController : FilterController<VehicleDTO>
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _vehicleService = serviceFactory.VehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _vehicleService.GetRangeAsync(offset, amount);
            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _vehicleService.GetAsync(id);
            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _vehicleService.SearchAsync(search);
            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] VehicleDTO vehicleDto)
        {
            var createdDto = await _vehicleService.CreateAsync(vehicleDto);
            if (createdDto != null)
            {
                return CreatedAtAction(nameof(Create), createdDto);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] VehicleDTO vehicleDto)
        {
            vehicleDto.Id = id;

            var result = await _vehicleService.UpdateAsync(vehicleDto);

            if (result != null)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _vehicleService.DeleteAsync(id);
            return NoContent();
        }
    }
}