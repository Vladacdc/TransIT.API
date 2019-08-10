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
            try
            {
                var result = await _malfunctionSubgroupService.GetRangeAsync(offset, amount);
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
                var result = await _malfunctionSubgroupService.GetAsync(id);
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
                var result = await _malfunctionSubgroupService.SearchAsync(search);
                return result != null
                    ? Json(result)
                    : null;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("api/v1/[controller]/getbygroupname")]
        public async Task<IActionResult> GetByGroupName(string groupName)
        {
            try
            {
                var result = await _malfunctionSubgroupService.GetByGroupNameAsync(groupName);
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
        public async Task<IActionResult> Create([FromBody] MalfunctionSubgroupDTO obj)
        {
            try
            {
                var createdEntity = await _malfunctionSubgroupService.CreateAsync(obj);
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
        public async Task<IActionResult> Update(int id, [FromBody] MalfunctionSubgroupDTO obj)
        {
            try
            {
                obj.Id = id;

                var result = await _malfunctionSubgroupService.UpdateAsync(obj);
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
            await _malfunctionSubgroupService.DeleteAsync(id);
            return NoContent();
        }
    }
}