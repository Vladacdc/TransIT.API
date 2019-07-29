using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.EndpointFilters.OnException;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public abstract class FilterController<TEntityDTO> : Controller
        where TEntityDTO : class, new()
    {
        protected const string DataTableTemplateUri = "~/api/v1/datatable/[controller]";
        protected readonly IFilterService<TEntityDTO> _filterService;

        protected FilterController(IFilterService<TEntityDTO> filterService)
        {
            _filterService = filterService;
        }
        
        [DataTableFilterExceptionFilter]
        [HttpPost(DataTableTemplateUri)]
        public virtual async Task<IActionResult> Filter(DataTableRequestDTO model) =>
            Json(
                ComposeDataTableResponseDTO(
                    await GetMappedEntitiesByModel(model),
                    model,
                    await _filterService.TotalRecordsAmountAsync()
                    )
                );

        protected async Task<IEnumerable<TEntityDTO>> GetMappedEntitiesByModel(DataTableRequestDTO model)
        {
            return await _filterService.GetQueriedAsync(model);
         
        }
        protected virtual DataTableResponseDTO ComposeDataTableResponseDTO(
            IEnumerable<TEntityDTO> res,
            DataTableRequestDTO model,
            ulong totalAmount,   
            string errorMessage = "") =>
            new DataTableResponseDTO
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
