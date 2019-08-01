using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using TransIT.BLL.Factories;

namespace TransIT.API.EndpointFilters.OnActionExecuting
{
    public class SetCurrentUserAttribute : ActionFilterAttribute
    {
        private readonly IServiceFactory _services;

        public SetCurrentUserAttribute(IServiceFactory services)
        {
            _services = services;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var id = context?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _services.UserService.UpdateCurrentUserId(id);
        }
    }
}