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
    public class MalfunctionSubGroupController : FilterController<MalfunctionSubgroupDTO>
    {
        private readonly IMalfunctionSubgroupService _malfunctionSubgroupService;

        public MalfunctionSubGroupController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _malfunctionSubgroupService = serviceFactory.MalfunctionSubgroupService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            return Json(await _malfunctionSubgroupService.GetRangeAsync(offset, amount));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            return Json(await _malfunctionSubgroupService.GetAsync(id));
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            return Json(await _malfunctionSubgroupService.SearchAsync(search));
        }

        [HttpGet]
        [Route("getbygroupname")]
        public async Task<IActionResult> GetByGroupName(string groupName)
        {
            return Json(await _malfunctionSubgroupService.GetByGroupNameAsync(groupName));
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] MalfunctionSubgroupDTO obj)
        {
            return CreatedAtAction(nameof(Create), await _malfunctionSubgroupService.CreateAsync(obj));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] MalfunctionSubgroupDTO obj)
        {
            obj.Id = id;
            return Json(await _malfunctionSubgroupService.UpdateAsync(obj));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _malfunctionSubgroupService.DeleteAsync(id);
            return NoContent();
        }
    }
}