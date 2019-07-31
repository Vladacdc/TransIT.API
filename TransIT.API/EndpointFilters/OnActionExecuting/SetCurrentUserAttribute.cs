using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using TransIT.BLL.Factory;

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
            string id = context?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            _services.UserService.UpdateCurrentUserId(id);
        }
    }
}
