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
    public class ActionTypeController : FilterController<ActionTypeDTO>
    {
        private readonly IActionTypeService _actionTypeService;

        public ActionTypeController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _actionTypeService = serviceFactory.ActionTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _actionTypeService.GetRangeAsync(offset, amount);
            if (result != null)
            {
                return Json(result);
            }

            return StatusCode(500);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _actionTypeService.GetAsync(id);
            if (result != null)
            {
                return Json(result);
            }

            return StatusCode(500);
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _actionTypeService.SearchAsync(search);
            if (result != null)
            {
                return Json(result);
            }

            return StatusCode(500);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] ActionTypeDTO actionTypeDTO)
        {
            var createdDTO = await _actionTypeService.CreateAsync(actionTypeDTO);

            if (createdDTO != null)
            {
                return CreatedAtAction(nameof(Create), createdDTO);
            }

            return StatusCode(500);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] ActionTypeDTO actionTypeDTO)
        {
            actionTypeDTO.Id = id;

            var result = await _actionTypeService.UpdateAsync(actionTypeDTO);

            if (result != null)
            {
                return NoContent();
            }

            return StatusCode(500);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _actionTypeService.DeleteAsync(id);
            return NoContent();
        }
    }
}