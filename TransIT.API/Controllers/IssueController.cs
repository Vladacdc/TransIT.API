using System.Collections.Generic;
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
using TransIT.BLL.Factory;
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
        private readonly IIssueService _issueService;

        private readonly IHubContext<IssueHub> _issueHub;

        public IssueController(
            IServiceFactory serviceFactory,
            IFilterServiceFactory filterServiceFactory,
            IHubContext<IssueHub> issueHub)
            : base(filterServiceFactory)
        {
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
                    return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await _issueService.GetAsync(id);
            return result != null
                ? Json(result)
                : (IActionResult)BadRequest();
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _issueService.SearchAsync(search);
            return result != null
                ? Json(result)
                : (IActionResult)BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IssueDTO obj)
        {
            var createdEntity = await _issueService.CreateAsync(obj);
            await _issueHub.Clients.Group(RoleNames.Engineer).SendAsync("ReceiveIssues");
            return createdEntity != null
                ? CreatedAtAction(nameof(Create), createdEntity)
                : (IActionResult)BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IssueDTO obj)
        {
            obj.Id = id;

            var result = await _issueService.UpdateAsync(obj);
            return result != null
                ? NoContent()
                : (IActionResult)BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _issueService.DeleteAsync(id);
            return NoContent();
        }

        [DataTableFilterExceptionFilter]
        [HttpPost("~/api/v1/datatable/[controller]")]
        public override async Task<IActionResult> Filter(DataTableRequestDTO model)
        {
            var isCustomer = User.FindFirst(RoleNames.Schema)?.Value == RoleNames.Register;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return Json(
                ComposeDataTableResponseDto(
                    await GetQueryiedForSpecificUser(model, userId, isCustomer),
                    model,
                    await GetTotalRecordsForSpecificUser(userId, isCustomer)));
        }

        private async Task<IEnumerable<IssueDTO>> GetForCustomer(uint offset, uint amount)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _issueService.GetRegisteredIssuesAsync(offset, amount, userId);
        }

        private async Task<IEnumerable<IssueDTO>> GetIssues(uint offset, uint amount)
        {
            return await _issueService.GetRangeAsync(offset, amount);
        }

        private async Task<IEnumerable<IssueDTO>> GetQueryiedForSpecificUser(
            DataTableRequestDTO model,
            string userId,
            bool isCustomer)
        {
            return isCustomer
                ? await _issueService.GetIssuesBySpecificUser(userId)
                : await _filterServiceFactory.GetService<IssueDTO>().GetQueriedAsync(model);
        }

        private async Task<ulong> GetTotalRecordsForSpecificUser(string userId, bool isCustomer)
        {
            return isCustomer
                ? await _issueService.GetTotalRecordsForSpecificUser(userId)
                : await _filterServiceFactory.GetService<IssueDTO>().TotalRecordsAmountAsync();
        }
    }
}