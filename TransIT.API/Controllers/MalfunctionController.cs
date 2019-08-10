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
            try
            {
                var result = await _malfunctionService.GetRangeAsync(offset, amount);
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
                var result = await _malfunctionService.GetAsync(id);
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
        [Route("api/v1/[controller]/getbysubgroupname")]
        public async Task<IActionResult> GetBySubgroupName(string subgroupName)
        {
            try
            {
                var result = await _malfunctionService.GetBySubgroupNameAsync(subgroupName);
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
                var result = await _malfunctionService.SearchAsync(search);
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
        public async Task<IActionResult> Create([FromBody] MalfunctionDTO obj)
        {
            try
            {
                var createdEntity = await _malfunctionService.CreateAsync(obj);
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
        public async Task<IActionResult> Update(int id, [FromBody] MalfunctionDTO obj)
        {
            try
            {
                obj.Id = id;

                var result = await _malfunctionService.UpdateAsync(obj);
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
            await _malfunctionService.DeleteAsync(id);
            return NoContent();
        }
    }
}