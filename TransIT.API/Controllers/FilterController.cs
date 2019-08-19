using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.EndpointFilters.OnException;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factories;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public abstract class FilterController<TEntityDto> : Controller
        where TEntityDto : class, new()
    {
        protected const string DataTableTemplateUri = "~/api/v1/datatable/[controller]";

        protected readonly IFilterServiceFactory _filterServiceFactory;

        protected FilterController(IFilterServiceFactory filterServiceFactory)
        {
            _filterServiceFactory = filterServiceFactory;
        }

        [HttpPost(DataTableTemplateUri)]
        public virtual async Task<IActionResult> Filter(DataTableRequestDTO model)
        {
            return Json(
                ComposeDataTableResponseDto(
                    await GetMappedEntitiesByModel(model),
                    model,
                    await _filterServiceFactory.GetService<TEntityDto>().TotalRecordsAmountAsync()));
        }

        protected async Task<IEnumerable<TEntityDto>> GetMappedEntitiesByModel(DataTableRequestDTO model)
        {
            return await _filterServiceFactory.GetService<TEntityDto>().GetQueriedAsync(model);
        }

        protected virtual DataTableResponseDTO ComposeDataTableResponseDto(
            IEnumerable<TEntityDto> res,
            DataTableRequestDTO model,
            ulong totalAmount,
            string errorMessage = "")
        {
            return new DataTableResponseDTO
            {
                Draw = (ulong)model.Draw,
                Data = res.ToArray(),
                RecordsTotal = totalAmount,
                RecordsFiltered =
                    (model.Filters != null
                     && model.Filters.Any())
                    || (model.Search != null
                        && !string.IsNullOrEmpty(model.Search.Value))
                        ? (ulong)res.Count()
                        : totalAmount,
                Error = errorMessage,
            };
        }
    }
}