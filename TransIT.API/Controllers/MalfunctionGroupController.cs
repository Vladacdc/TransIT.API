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
    public class MalfunctionGroupController : FilterController<MalfunctionGroupDTO>
    {
        private readonly IMalfunctionGroupService _malfunctionGroupService;

        public MalfunctionGroupController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _malfunctionGroupService = serviceFactory.MalfunctionGroupService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            return Json(await _malfunctionGroupService.GetRangeAsync(offset, amount));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            return Json(await _malfunctionGroupService.GetAsync(id));
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            return Json(await _malfunctionGroupService.SearchAsync(search));
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] MalfunctionGroupDTO obj)
        {
            return CreatedAtAction(nameof(Create), await _malfunctionGroupService.CreateAsync(obj));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] MalfunctionGroupDTO obj)
        {
            obj.Id = id;
            return Json(await _malfunctionGroupService.UpdateAsync(obj));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _malfunctionGroupService.DeleteAsync(id);
            return NoContent();
        }
    }
}