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
    public class TransitionController : FilterController<TransitionDTO>
    {
        private readonly ITransitionService _transitionService;

        public TransitionController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _transitionService = serviceFactory.TransitionService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _transitionService.GetRangeAsync(offset, amount);
            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _transitionService.GetAsync(id);
            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _transitionService.SearchAsync(search);
            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] TransitionDTO transitionDto)
        {
            var createdDto = await _transitionService.CreateAsync(transitionDto);
            if (createdDto != null)
            {
                return CreatedAtAction(nameof(Create), createdDto);
            }

            return BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] TransitionDTO transitionDto)
        {
            transitionDto.Id = id;

            var result = await _transitionService.UpdateAsync(transitionDto);

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
            await _transitionService.DeleteAsync(id);
            return NoContent();
        }
    }
}