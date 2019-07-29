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
using Microsoft.AspNetCore.Cors;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
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
        }

        private async Task<IEnumerable<IssueDTO>> GetQueryiedForSpecificUser(
            DataTableRequestDTO model,
            string userId,
            bool isCustomer)
        {
            return isCustomer
                    ? await _serviceFactory.IssueService.FilterAsync(userId)
                    : await _filterService.GetQueriedAsync(model);
        }

        private async Task<ulong> GetTotalRecordsForSpecificUser(string userId, bool isCustomer)
        {
            return isCustomer
                ? await _serviceFactory.IssueService.GetTotalRecordsForSpecificUser(userId)
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
                : (IActionResult)BadRequest();
        }

        [HttpGet("/search")]
        public virtual async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _serviceFactory.IssueService.SearchAsync(search);
            return result != null
                ? Json(result)
                : (IActionResult)BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IssueDTO obj)
        {
            var createdEntity = await _serviceFactory.IssueService.CreateAsync(obj);
            await _issueHub.Clients.Group(ROLE.ENGINEER).SendAsync("ReceiveIssues");
            return createdEntity != null
                ? CreatedAtAction(nameof(Create), createdEntity)
                : (IActionResult)BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] IssueDTO obj)
        {
            obj.Id = id;

            var result = await _serviceFactory.IssueService.UpdateAsync(obj);
            return result != null
                ? NoContent()
                : (IActionResult)BadRequest();
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
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return Json(
                ComposeDataTableResponseDTO(
                    await GetQueryiedForSpecificUser(model, userId, isCustomer),
                    model,
                    await GetTotalRecordsForSpecificUser(userId, isCustomer)
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
                Draw = (ulong)model.Draw,
                Data = res.ToArray(),
                RecordsTotal = totalAmount,
                RecordsFiltered =
                    model.Filters != null
                    && model.Filters.Any()
                    || model.Search != null
                    && !string.IsNullOrEmpty(model.Search.Value)
                        ? (ulong)res.Count()
                        : totalAmount,
                Error = errorMessage
            };
        }
    }
}