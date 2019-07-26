using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.EndpointFilters.OnException;
using Microsoft.AspNetCore.SignalR;
using TransIT.API.Extensions;
using TransIT.API.Hubs;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ENGINEER,REGISTER,ANALYST")]
    public class IssueController : DataController<Issue, IssueDTO>
    {
        private readonly IIssueService _issueService;
        private readonly IHubContext<IssueHub> _issueHub;
        
        public IssueController(
            IMapper mapper,
            IIssueService issueService,
            IFilterService<Issue> odService,
            IHubContext<IssueHub> issueHub
            ) : base(mapper, issueService, odService)
        {
            _issueService = issueService;
            _issueHub = issueHub;
        }

        [DataTableFilterExceptionFilter]
        [HttpPost(DataTableTemplateUri)]
        public override async Task<IActionResult> Filter(DataTableRequestDTO model)
        {
            //var isCustomer = User.FindFirst(ROLE.ROLE_SCHEMA)?.Value == ROLE.REGISTER;
            //var userId = GetUserId();

            //return Json(
            //    ComposeDataTableResponseDTO(
            //        await GetQueryiedForSpecificUser(model, userId, isCustomer),
            //        model,
            //        GetTotalRecordsForSpecificUser(userId, isCustomer)
            //        )
            //    );
            return null;
        }
        
        private async Task<IEnumerable<IssueDTO>> GetQueryiedForSpecificUser(
            DataTableRequestDTO model,
            string userId,
            bool isCustomer) =>
            _mapper.Map<IEnumerable<IssueDTO>>(
                isCustomer
                    ? await _filterService.GetQueriedWithWhereAsync(model, x => x.CreatedById == userId)
                    : await _filterService.GetQueriedAsync(model)
                );

        private ulong GetTotalRecordsForSpecificUser(
            string userId,
            bool isCustomer) =>
            isCustomer
                ? _filterService.TotalRecordsAmount(x => x.CreatedById == userId)
                : _filterService.TotalRecordsAmount();

        [HttpGet]
        public override async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            switch (User.FindFirst(ROLE.ROLE_SCHEMA)?.Value)
            {
                case ROLE.REGISTER:
                    return Json(await GetForCustomer(offset, amount));
                case ROLE.ENGINEER:                        
                case ROLE.ANALYST:
                    return Json(await GetIssues(offset, amount));
                default:
                    return BadRequest();
            }
        }

        [HttpPost]
        public override async Task<IActionResult> Create([FromBody] IssueDTO obj)
        {
            IActionResult result = await base.Create(obj);
            await _issueHub.Clients.Group(ROLE.ENGINEER).SendAsync("ReceiveIssues");
            return result;
        }

        private async Task<IEnumerable<IssueDTO>> GetForCustomer(uint offset, uint amount)
        {
            //string userId = GetUserId();
            //var res = await _issueService.GetRegisteredIssuesAsync(offset, amount, userId);
            //return res != null
            //    ? _mapper.Map<IEnumerable<IssueDTO>>(res)
            //    : null;
            return null;
        }

        private async Task<IEnumerable<IssueDTO>> GetIssues(uint offset, uint amount)
        {
            var res = await _issueService.GetRangeAsync(offset, amount);
            return res != null
                ? _mapper.Map<IEnumerable<IssueDTO>>(res)
                : null;
        }
    }
}
