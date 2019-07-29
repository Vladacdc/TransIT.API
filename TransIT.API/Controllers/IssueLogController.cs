using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.EndpointFilters.OnException;
using TransIT.BLL.Services;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factory;
using TransIT.DAL.Models.Entities;
using Microsoft.AspNetCore.Cors;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ADMIN,ENGINEER,REGISTER,ANALYST")]
    public class IssueLogController : Controller
    {
        private readonly IServiceFactory _serviceFactory;

        private readonly IFilterService<IssueLogDTO> _filterService;

        private const string IssueLogByIssueUrl = "~/api/v1/" + nameof(Issue) + "/{issueId}/" + nameof(IssueLog);

        private const string DataTableTemplateIssueLogByIssueUrl =
            "~/api/v1/datatable/" + nameof(Issue) + "/{issueId}/" + nameof(IssueLog);

        public IssueLogController(
            IServiceFactory serviceFactory,
            IFilterService<IssueLogDTO> filterService)
        {
            _serviceFactory = serviceFactory;
            _filterService = filterService;
        }

        [HttpGet(IssueLogByIssueUrl)]
        public virtual async Task<IActionResult> GetByIssue(int issueId)
        {
            var result = await _serviceFactory.IssueLogService.GetRangeByIssueIdAsync(issueId);
            return result != null
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        private async Task<IEnumerable<IssueLogDTO>> GetMappedEntitiesByIssueId(int issueId, DataTableRequestDTO model)
        {
            return await _serviceFactory.IssueLogService.GetRangeByIssueIdAsync(issueId);
        }

        [HttpGet]
        public virtual async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _serviceFactory.IssueLogService.GetRangeAsync(offset, amount);
            return result != null
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await _serviceFactory.IssueLogService.GetAsync(id);
            return result != null
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _serviceFactory.IssueLogService.SearchAsync(search);
            return result != null
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IssueLogDTO obj)
        {
            var result = await _serviceFactory.IssueLogService.CreateAsync(obj);
            return result != null
                ? CreatedAtAction(nameof(Create), result)
                : (IActionResult) BadRequest();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] IssueLogDTO obj)
        {
            obj.Id = id;

            var result = await _serviceFactory.IssueLogService.UpdateAsync(obj);
            return result != null
                ? NoContent()
                : (IActionResult) BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceFactory.IssueLogService.DeleteAsync(id);
            return NoContent();
        }

        [DataTableFilterExceptionFilter]
        [HttpPost(DataTableTemplateIssueLogByIssueUrl)]
        public virtual async Task<IActionResult> Filter(
            int issueId,
            DataTableRequestDTO model)
        {
            var dtResponse = ComposeDataTableResponseDTO(
                await GetMappedEntitiesByIssueId(issueId, model),
                model,
                _filterService.TotalRecordsAmount()
            );
            dtResponse.RecordsFiltered = (ulong) dtResponse.Data.LongLength;
            return Json(dtResponse);
        }

        protected async Task<IEnumerable<IssueLogDTO>> GetMappedEntitiesByModel(DataTableRequestDTO model)
        {
            return await _filterService.GetQueriedAsync(model);
        }

        protected virtual DataTableResponseDTO ComposeDataTableResponseDTO(
            IEnumerable<IssueLogDTO> res,
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