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
    [Authorize(Roles = "ADMIN,ENGINEER,ANALYST")]
    public class LocationController : FilterController<LocationDTO>
    {
        private readonly ILocationService _locationService;

        public LocationController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _locationService = serviceFactory.LocationService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _locationService.GetRangeAsync(offset, amount);
            return result != null
                ? Json(result)
                : null;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await _locationService.GetAsync(id);
            return result != null
                ? Json(result)
                : null;
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _locationService.SearchAsync(search);
            return result != null
                ? Json(result)
                : null;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] LocationDTO obj)
        {
            var createdEntity = await _locationService.CreateAsync(obj);
            return createdEntity != null
                ? CreatedAtAction(nameof(Create), createdEntity)
                : null;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] LocationDTO obj)
        {
            obj.Id = id;

            var result = await _locationService.UpdateAsync(obj);
            return result != null
                ? NoContent()
                : null;
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _locationService.DeleteAsync(id);
            return NoContent();
        }
    }
}