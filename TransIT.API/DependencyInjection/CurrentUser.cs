using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using TransIT.DAL.Models.DependencyInjection;

namespace TransIT.API.DependencyInjection
{
    /// <summary>
    /// Implements current user as Asp.Net user.
    /// </summary>
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _httpContext;

        public CurrentUser(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public string CurrentUserId
        {
            get
            {
                return _httpContext?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            }
        }
    }
}
