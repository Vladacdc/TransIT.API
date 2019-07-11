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
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Models.ViewModels;

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
        public override async Task<IActionResult> Filter(DataTableRequestViewModel model)
        {
            var isCustomer = User.FindFirst(RoleConsts.RoleSchema)?.Value == RoleConsts.Register;
            var userId = GetUserId();

            return Json(
                ComposeDataTableResponseViewModel(
                    await GetQueryiedForSpecificUser(model, userId, isCustomer),
                    model,
                    GetTotalRecordsForSpecificUser(userId, isCustomer)
                    )
                );
        }
        
        private async Task<IEnumerable<IssueDTO>> GetQueryiedForSpecificUser(
            DataTableRequestViewModel model,
            int userId,
            bool isCustomer) =>
            _mapper.Map<IEnumerable<IssueDTO>>(
                isCustomer
                    ? await _filterService.GetQueriedWithWhereAsync(model, x => x.CreateId == userId)
                    : await _filterService.GetQueriedAsync(model)
                );

        private ulong GetTotalRecordsForSpecificUser(
            int userId,
            bool isCustomer) =>
            isCustomer
                ? _filterService.TotalRecordsAmount(x => x.CreateId == userId)
                : _filterService.TotalRecordsAmount();

        [HttpGet]
        public override async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            switch (User.FindFirst(RoleConsts.RoleSchema)?.Value)
            {
                case RoleConsts.Register:
                    return Json(await GetForCustomer(offset, amount));
                case RoleConsts.Engineer:                        
                case RoleConsts.Analyst:
                    return Json(await GetIssues(offset, amount));
                default:
                    return BadRequest();
            }
        }

        [HttpPost]
        public override async Task<IActionResult> Create([FromBody] IssueDTO obj)
        {
            IActionResult result = await base.Create(obj);
            await _issueHub.Clients.Group(RoleConsts.Engineer).SendAsync("ReceiveIssues");
            return result;
        }

        private async Task<IEnumerable<IssueDTO>> GetForCustomer(uint offset, uint amount)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var res = await _issueService.GetRegisteredIssuesAsync(offset, amount, userId);
            return res != null
                ? _mapper.Map<IEnumerable<IssueDTO>>(res)
                : null;
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
