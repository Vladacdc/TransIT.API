using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.EndpointFilters.OnException;
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
    public class IssueLogController : FilterController<IssueLogDTO>
    {
        private const string IssueLogByIssueUrl = "~/api/v1/Issue/{issueId}/IssueLog";

        private const string DataTableTemplateIssueLogByIssueUrl = "~/api/v1/datatable/Issue/{issueId}/IssueLog";

        private readonly IIssueLogService _issueLogService;

        public IssueLogController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _issueLogService = serviceFactory.IssueLogService;
        }

        [HttpGet(IssueLogByIssueUrl)]
        public virtual async Task<IActionResult> GetByIssue(int issueId)
        {
            try
            {
                var result = await _issueLogService.GetRangeByIssueIdAsync(issueId);
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
        public virtual async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            try
            {
                var result = await _issueLogService.GetRangeAsync(offset, amount);
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
                var result = await _issueLogService.GetAsync(id);
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
                var result = await _issueLogService.SearchAsync(search);
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
        public async Task<IActionResult> Create([FromBody] IssueLogDTO obj)
        {
            try
            {
                var result = await _issueLogService.CreateAsync(obj);
                return result != null
                    ? CreatedAtAction(nameof(Create), result)
                    : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] IssueLogDTO obj)
        {
            try
            {
                obj.Id = id;

                var result = await _issueLogService.UpdateAsync(obj);
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
            await _issueLogService.DeleteAsync(id);
            return NoContent();
        }

        [DataTableFilterExceptionFilter]
        [HttpPost(DataTableTemplateIssueLogByIssueUrl)]
        public virtual async Task<IActionResult> Filter(
            int issueId,
            DataTableRequestDTO model)
        {
            var dtResponse = ComposeDataTableResponseDto(
                await GetMappedEntitiesByIssueId(issueId),
                model,
                await _filterServiceFactory.GetService<IssueLogDTO>().TotalRecordsAmountAsync());
            dtResponse.RecordsFiltered = (ulong)dtResponse.Data.LongLength;
            return Json(dtResponse);
        }

        private async Task<IEnumerable<IssueLogDTO>> GetMappedEntitiesByIssueId(int issueId)
        {
            return await _issueLogService.GetRangeByIssueIdAsync(issueId);
        }
    }
}