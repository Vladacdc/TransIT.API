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
    public class MalfunctionController : FilterController<MalfunctionDTO>
    {
        private readonly IMalfunctionService _malfunctionService;

        public MalfunctionController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _malfunctionService = serviceFactory.MalfunctionService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            return Json(await _malfunctionService.GetRangeAsync(offset, amount));
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            return Json(await _malfunctionService.GetAsync(id));
        }

        [HttpGet]
        [Route("getbysubgroupname")]
        public async Task<IActionResult> GetBySubgroupName(string subgroupName)
        {
            return Json(await _malfunctionService.GetBySubgroupNameAsync(subgroupName));
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            return Json(await _malfunctionService.SearchAsync(search));
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] MalfunctionDTO obj)
        {
            return CreatedAtAction(nameof(Create), await _malfunctionService.CreateAsync(obj));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] MalfunctionDTO obj)
        {
            obj.Id = id;
            return Json(await _malfunctionService.UpdateAsync(obj));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _malfunctionService.DeleteAsync(id);
            return NoContent();
        }
    }
}