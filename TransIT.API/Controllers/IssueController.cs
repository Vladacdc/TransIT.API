using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.EndpointFilters.OnException;
using Microsoft.AspNetCore.SignalR;
using TransIT.API.Extensions;
using TransIT.API.Hubs;
using TransIT.BLL.Services;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factory;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ENGINEER,REGISTER,ANALYST")]
    public class IssueController : Controller
    {
        private readonly IServiceFactory _serviceFactory;

        private readonly IFilterService<IssueDTO> _filterService;

        private readonly IHubContext<IssueHub> _issueHub;

        public IssueController(
            IServiceFactory serviceFactory,
            IFilterService<IssueDTO> filterService,
            IHubContext<IssueHub> issueHub)
        {
            _serviceFactory = serviceFactory;
            _filterService = filterService;
            _issueHub = issueHub;
            //_mapper = mapper;
        }

        private async Task<IEnumerable<IssueDTO>> GetQueryiedForSpecificUser(
            DataTableRequestDTO model,
            int userId,
            bool isCustomer)
        {
            return isCustomer
                    ? await _filterService.GetQueriedWithWhereAsync(model, x => x.CreateId == userId)
                    : await _filterService.GetQueriedAsync(model)
                ;
        }

        private ulong GetTotalRecordsForSpecificUser(int userId, bool isCustomer)
        {
            return isCustomer
                ? _filterService.TotalRecordsAmount(x => x.CreateId == userId)
                : _filterService.TotalRecordsAmount();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
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

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            var result = await _serviceFactory.IssueService.GetAsync(id);
            return result != null
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _serviceFactory.IssueService.SearchAsync(search);
            return result != null
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IssueDTO obj)
        {

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var createdEntity = await _serviceFactory.IssueService.CreateAsync(obj, userId);
            await _issueHub.Clients.Group(ROLE.ENGINEER).SendAsync("ReceiveIssues");
            return createdEntity != null
                ? CreatedAtAction(nameof(Create), createdEntity)
                : (IActionResult) BadRequest();
        }

        public async Task<IActionResult> Update(int id, [FromBody] IssueDTO obj)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            obj.Id = id;

            var result = await _serviceFactory.IssueService.UpdateAsync(obj, userId);
            return result != null
                ? NoContent()
                : (IActionResult) BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _serviceFactory.IssueService.DeleteAsync(id);
            return NoContent();
        }

        private async Task<IEnumerable<IssueDTO>> GetForCustomer(uint offset, uint amount)
        {
            int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return await _serviceFactory.IssueService.GetRegisteredIssuesAsync(offset, amount, userId);
        }

        private async Task<IEnumerable<IssueDTO>> GetIssues(uint offset, uint amount)
        {
            return await _serviceFactory.IssueService.GetRangeAsync(offset, amount);
        }

        [DataTableFilterExceptionFilter]
        [HttpPost("~/api/v1/datatable/[controller]")]
        public async Task<IActionResult> Filter(DataTableRequestDTO model)
        {
            var isCustomer = User.FindFirst(ROLE.ROLE_SCHEMA)?.Value == ROLE.REGISTER;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Json(
                ComposeDataTableResponseDTO(
                    await GetQueryiedForSpecificUser(model, userId, isCustomer),
                    model,
                    GetTotalRecordsForSpecificUser(userId, isCustomer)
                )
            );
        }

        protected async Task<IEnumerable<IssueDTO>> GetMappedEntitiesByModel(DataTableRequestDTO model)
        {
            return await _filterService.GetQueriedAsync(model);
        }

        protected virtual DataTableResponseDTO ComposeDataTableResponseDTO(
            IEnumerable<IssueDTO> res,
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