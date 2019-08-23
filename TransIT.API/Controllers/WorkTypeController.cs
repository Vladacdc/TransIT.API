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
    public class WorkTypeController : FilterController<WorkTypeDTO>
    {
        private readonly IWorkTypeService _workTypeService;

        public WorkTypeController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _workTypeService = serviceFactory.WorkTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            return Json(await _workTypeService.GetRangeAsync(offset, amount));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Json(await _workTypeService.GetAsync(id));
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            return Json(await _workTypeService.SearchAsync(search));
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] WorkTypeDTO workTypeDto)
        {
            return CreatedAtAction(nameof(Create), await _workTypeService.CreateAsync(workTypeDto));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] WorkTypeDTO workTypeDto)
        {
            workTypeDto.Id = id;
            return Json(await _workTypeService.UpdateAsync(workTypeDto));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _workTypeService.DeleteAsync(id);
            return NoContent();
        }
    }
}
