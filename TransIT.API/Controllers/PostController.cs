using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factory;
using TransIT.BLL.Services;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ADMIN")]
    public class PostController : FilterController<PostDTO>
    {
        private readonly IServiceFactory _serviceFactory;

        public PostController(
            IServiceFactory serviceFactory,
            IFilterService<PostDTO> filterService)
            : base(filterService)
        {
            _serviceFactory = serviceFactory;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _serviceFactory.PostService.GetRangeAsync(offset, amount);
            return result != null
                ? Json(result)
                : (IActionResult)BadRequest();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await _serviceFactory.PostService.GetAsync(id);
            return result != null
                ? Json(result)
                : (IActionResult)BadRequest();
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _serviceFactory.PostService.SearchAsync(search);
            return result != null
                ? Json(result)
                : (IActionResult)BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] PostDTO obj)
        {
            var createdEntity = await _serviceFactory.PostService.CreateAsync(obj);
            return createdEntity != null
                ? CreatedAtAction(nameof(Create), createdEntity)
                : (IActionResult)BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] PostDTO obj)
        {
            obj.Id = id;

            var result = await _serviceFactory.PostService.UpdateAsync(obj);
            return result != null
                ? NoContent()
                : (IActionResult)BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceFactory.PostService.DeleteAsync(id);
            return NoContent();
        }
    }
}