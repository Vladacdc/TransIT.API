using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.EndpointFilters.OnException;
using TransIT.BLL.Services;
using TransIT.DAL.Models.Entities.Abstractions;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public abstract class DataController<TId, TEntity, TEntityDTO> : FilterController<TId, TEntity, TEntityDTO>
        where TEntity : class, IAuditableEntity, new()
        where TEntityDTO : class
    {
        private readonly ICrudService<TId, TEntity> _dataService;
        
        public DataController(
            IMapper mapper,
            ICrudService<TId, TEntity> dataService,
            IFilterService<TId, TEntity> filterService) : base(filterService, mapper)
        {
            _dataService = dataService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _dataService.GetRangeAsync(offset, amount);
            return result != null
                ? Json(_mapper.Map<IEnumerable<TEntityDTO>>(result))
                : (IActionResult) BadRequest();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(TId id)
        {
            var result = await _dataService.GetAsync(id);
            return result != null
                ? Json(_mapper.Map<TEntityDTO>(result))
                : (IActionResult) BadRequest();
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _dataService.SearchAsync(search);
            return result != null
                ? Json(_mapper.Map<IEnumerable<TEntityDTO>>(result))
                : (IActionResult) BadRequest();
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TEntityDTO obj)
        {
            var entity = _mapper.Map<TEntity>(obj);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            entity.ModifiedById = userId;
            entity.CreatedById = userId;

            var createdEntity = await _dataService.CreateAsync(entity);
            return createdEntity != null
                ? CreatedAtAction(nameof(Create), _mapper.Map<TEntityDTO>(createdEntity))
                : (IActionResult) BadRequest();
        }

        [UpdateExceptionFilter]
        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(TId id, [FromBody] TEntityDTO obj)
        {
            var entity = _mapper.Map<TEntity>(obj);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //entity.Id = id; 
            entity.ModifiedById = userId;

            var result = await _dataService.UpdateAsync(entity);
            return result != null
                ? NoContent()
                : (IActionResult) BadRequest();
        }

        [DeleteExceptionFilter]
        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(TId id)
        {
            await _dataService.DeleteAsync(id);
            return NoContent();
        }

        protected string GetUserId() => User.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
