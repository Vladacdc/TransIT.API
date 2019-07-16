using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.EndpointFilters.OnException;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,REGISTER,ANALYST")]
    public class IssueLogController : DataController<IssueLog, IssueLogDTO>
    {
        private readonly IIssueLogService _issueLogService;
        private const string IssueLogByIssueUrl = "~/api/v1/" + nameof(Issue) + "/{issueId}/" + nameof(IssueLog); 
        private const string DataTableTemplateIssueLogByIssueUrl = "~/api/v1/datatable/" + nameof(Issue) + "/{issueId}/" + nameof(IssueLog); 

        public IssueLogController(
            IMapper mapper,
            IIssueLogService issueLogService,
            IFilterService<IssueLog> odService
            ) : base(mapper, issueLogService, odService)
        {
            _issueLogService = issueLogService;
        }

        [HttpGet(IssueLogByIssueUrl)]
        public virtual async Task<IActionResult> GetByIssue(int issueId)
        {
            var result = await _issueLogService.GetRangeByIssueIdAsync(issueId);
            return result != null
                ? Json(_mapper.Map<IEnumerable<IssueLogDTO>>(result))
                : (IActionResult) BadRequest();
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

        private async Task<IEnumerable<IssueLogDTO>> GetMappedEntitiesByIssueId(int issueId, DataTableRequestDTO model) =>
            _mapper.Map<IEnumerable<IssueLogDTO>>(
                await _filterService.GetQueriedWithWhereAsync(
                    model, 
                    x => x.IssueId == issueId
                    )
                );

        [HttpPost]
        public override async Task<IActionResult> Create([FromBody] IssueLogDTO obj)
        {
            var entity = _mapper.Map<IssueLog>(obj);
            entity.CreateId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            entity = await _issueLogService.CreateAsync(entity);
            return entity != null
                ? CreatedAtAction(nameof(Create), _mapper.Map<IssueLogDTO>(entity))
                : (IActionResult) BadRequest();
        }
    }
}
