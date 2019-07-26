using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransIT.API.EndpointFilters.OnException;
using TransIT.BLL.Services;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factory;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,ANALYST")]
    public class LocationController : Controller
    {
        private readonly IServiceFactory _serviceFactory;

        private readonly IFilterService<LocationDTO> _filterService;

        public LocationController(
            IServiceFactory serviceFactory,
            IFilterService<LocationDTO> filterService)
        {
            _serviceFactory = serviceFactory;
            _filterService = filterService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _serviceFactory.LocationService.GetRangeAsync(offset, amount);
            return result != null
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await _serviceFactory.LocationService.GetAsync(id);
            return result != null
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _serviceFactory.LocationService.SearchAsync(search);
            return result != null
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] LocationDTO obj)
        {
            var createdEntity = await _serviceFactory.LocationService.CreateAsync(obj);
            return createdEntity != null
                ? CreatedAtAction(nameof(Create), createdEntity)
                : (IActionResult) BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] LocationDTO obj)
        {
            obj.Id = id;

            var result = await _serviceFactory.LocationService.UpdateAsync(obj);
            return result != null
                ? NoContent()
                : (IActionResult) BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceFactory.LocationService.DeleteAsync(id);
            return NoContent();
        }

        [DataTableFilterExceptionFilter]
        [HttpPost("~/api/v1/datatable/[controller]")]
        public virtual async Task<IActionResult> Filter(DataTableRequestDTO model)
        {
            return Json(
                ComposeDataTableResponseDTO(
                    await GetMappedEntitiesByModel(model),
                    model,
                    _filterService.TotalRecordsAmount()
                )
            );
        }

        protected async Task<IEnumerable<LocationDTO>> GetMappedEntitiesByModel(DataTableRequestDTO model)
        {
            return await _filterService.GetQueriedAsync(model);
        }

        protected virtual DataTableResponseDTO ComposeDataTableResponseDTO(
            IEnumerable<LocationDTO> res,
            DataTableRequestDTO model,
            ulong totalAmount,
            string errorMessage = "")
        {
            return new DataTableResponseDTO
            {
                Draw = (ulong) model.Draw,
                Data = res.ToArray(),
                RecordsTotal = totalAmount,
                RecordsFiltered =
                    model.Filters != null
                    && model.Filters.Any()
                    || model.Search != null
                    && !string.IsNullOrEmpty(model.Search.Value)
                        ? (ulong) res.Count()
                        : totalAmount,
                Error = errorMessage
            };
        }
    }
}