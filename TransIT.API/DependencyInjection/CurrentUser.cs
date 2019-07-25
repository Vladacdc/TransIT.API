using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TransIT.DAL.Models.DependencyInjection;

namespace TransIT.API.DependencyInjection
{
    /// <summary>
    /// Implements current user as Asp.Net user
    /// </summary>
    public class CurrentUser : IUser
    {
        private readonly IHttpContextAccessor _httpContext;

        public CurrentUser(IHttpContextAccessor httpContext)
        {
            this._httpContext = httpContext;
        }
        public string CurrentUserId
        {
            get
            {
                return _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }
    }
}
