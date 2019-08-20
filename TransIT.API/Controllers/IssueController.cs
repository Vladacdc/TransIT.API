using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using TransIT.API.EndpointFilters.OnException;
using TransIT.API.Extensions;
using TransIT.API.Hubs;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factories;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ENGINEER,REGISTER,ANALYST")]
    public class IssueController : FilterController<IssueDTO>
    {
        private readonly IUserService _userService;
        private readonly IIssueService _issueService;
        private readonly IFilterService<IssueDTO> _filterService;
        private readonly IHubContext<IssueHub> _issueHub;

        public IssueController(
            IServiceFactory serviceFactory,
            IFilterServiceFactory filterServiceFactory,
            IHubContext<IssueHub> issueHub)
            : base(filterServiceFactory)
        {
            _userService = serviceFactory.UserService;
            _filterService = filterServiceFactory.GetService<IssueDTO>();
            _issueService = serviceFactory.IssueService;
            _issueHub = issueHub;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            switch (User.FindFirst(RoleNames.Schema)?.Value)
            {
                case RoleNames.Register:
                    return Json(await GetForCustomer(offset, amount));
                case RoleNames.Engineer:
                case RoleNames.Analyst:
                    return Json(await GetIssues(offset, amount));
                default:
                    return null;
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            return Json(await _issueService.GetAsync(id));
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            return Json(await _issueService.SearchAsync(search));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IssueDTO obj)
        {
            var createdEntity = await _issueService.CreateAsync(obj);
            await _issueHub.Clients.Group(RoleNames.Engineer).SendAsync("ReceiveIssues");
            return CreatedAtAction(nameof(Create), createdEntity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IssueDTO obj)
        {
            obj.Id = id;
            return Json(await _issueService.UpdateAsync(obj));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN,REGISTER")]
        public async Task<IActionResult> Delete(int id)
        {
            await _issueService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("~/api/v1/datatable/[controller]")]
        public override async Task<IActionResult> Filter(DataTableRequestDTO model)
        {
            var isCustomer = User.FindFirst(RoleNames.Schema)?.Value == RoleNames.Register;

            return Json(
                this.ComposeDataTableResponseDto(
                    await GetQueryiedForCurrentUser(model, isCustomer),
                    model,
                    await GetTotalRecordsForCurrentUser(isCustomer)));
        }

        private async Task<IEnumerable<IssueDTO>> GetForCustomer(uint offset, uint amount)
        {
            return await _issueService.GetRegisteredIssuesAsync(offset, amount);
        }

        private async Task<IEnumerable<IssueDTO>> GetIssues(uint offset, uint amount)
        {
            return await _issueService.GetRangeAsync(offset, amount);
        }

        private async Task<IEnumerable<IssueDTO>> GetQueryiedForCurrentUser(
            DataTableRequestDTO model,
            bool isCustomer)
        {
            if (isCustomer)
            {
                if (model.Filters == null)
                {
                    model.Filters = new List<DataTableRequestDTO.FilterType>();
                }

                model.Filters.Add(new DataTableRequestDTO.FilterType()
                {
                    EntityPropertyPath = "CreatedById",
                    Operator = "==",
                    Value = _userService.GetCurrentUserId()
                });
            }
            return await _filterService.GetQueriedAsync(model);
        }

        private async Task<ulong> GetTotalRecordsForCurrentUser(bool isCustomer)
        {
            return isCustomer
                ? await _issueService.GetTotalRecordsForCurrentUser()
                : await _filterService.TotalRecordsAmountAsync();
        }

        protected override DataTableResponseDTO ComposeDataTableResponseDto(
            IEnumerable<IssueDTO> res,
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
                    (model.Filters?.Count > 1)
                    || (!string.IsNullOrEmpty(model.Search?.Value))
                        ? (ulong)res.Count()
                        : totalAmount,
                Error = errorMessage,
            };
        }
    }
}