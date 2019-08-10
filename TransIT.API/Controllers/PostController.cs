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
    [Authorize(Roles = "ADMIN")]
    public class PostController : FilterController<PostDTO>
    {
        private readonly IPostService _postService;

        public PostController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _postService = serviceFactory.PostService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            try
            {
                var result = await _postService.GetRangeAsync(offset, amount);
                return result != null
                    ? Json(result)
                    : null;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            try
            {
                var result = await _postService.GetAsync(id);
                return result != null
                    ? Json(result)
                    : null;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            try
            {
                var result = await _postService.SearchAsync(search);
                return result != null
                    ? Json(result)
                    : null;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] PostDTO obj)
        {
            try
            {
                var createdEntity = await _postService.CreateAsync(obj);
                return createdEntity != null
                    ? CreatedAtAction(nameof(Create), createdEntity)
                    : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] PostDTO obj)
        {
            try
            {
                obj.Id = id;

                var result = await _postService.UpdateAsync(obj);
                return result != null
                    ? NoContent()
                    : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeleteAsync(id);
            return NoContent();
        }
    }
}