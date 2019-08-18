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
    [Authorize(Roles = "ADMIN,ANALYST,ENGINEER")]
    public class VehicleTypeController : FilterController<VehicleTypeDTO>
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleTypeController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _vehicleTypeService = serviceFactory.VehicleTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _vehicleTypeService.GetRangeAsync(offset, amount);
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
            var result = await _vehicleTypeService.GetAsync(id);
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
            var result = await _vehicleTypeService.SearchAsync(search);
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
        public async Task<IActionResult> Create([FromBody] VehicleTypeDTO vehicleTypeDto)
        {
            var createdDto = await _vehicleTypeService.CreateAsync(vehicleTypeDto);
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
        public async Task<IActionResult> Update(int id, [FromBody] VehicleTypeDTO vehicleTypeDto)
        {
            vehicleTypeDto.Id = id;

            var result = await _vehicleTypeService.UpdateAsync(vehicleTypeDto);

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
            await _vehicleTypeService.DeleteAsync(id);
            return NoContent();
        }
    }
}