using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransIT.API.EndpointFilters.OnException;
using TransIT.BLL.Services;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factory;
using Microsoft.AspNetCore.Cors;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ADMIN,ENGINEER,ANALYST")]
    public class MalfunctionSubGroupController : FilterController<MalfunctionSubgroupDTO>
    {
        private readonly IServiceFactory _serviceFactory;

        public MalfunctionSubGroupController(
            IServiceFactory serviceFactory,
            IFilterService<MalfunctionSubgroupDTO> filterService) : base(filterService)
        {
            _serviceFactory = serviceFactory;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _serviceFactory.MalfunctionSubgroupService.GetRangeAsync(offset, amount);
            return result != null
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await _serviceFactory.MalfunctionSubgroupService.GetAsync(id);
            return result != null
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _serviceFactory.MalfunctionSubgroupService.SearchAsync(search);
            return result != null
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] MalfunctionSubgroupDTO obj)
        {
            var createdEntity = await _serviceFactory.MalfunctionSubgroupService.CreateAsync(obj);
            return createdEntity != null
                ? CreatedAtAction(nameof(Create), createdEntity)
                : (IActionResult) BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] MalfunctionSubgroupDTO obj)
        {
            obj.Id = id;

            var result = await _serviceFactory.MalfunctionSubgroupService.UpdateAsync(obj);
            return result != null
                ? NoContent()
                : (IActionResult) BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceFactory.MalfunctionSubgroupService.DeleteAsync(id);
            return NoContent();
        }
    }
}