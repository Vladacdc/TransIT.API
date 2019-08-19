using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Factories;
using TransIT.BLL.Services.Interfaces;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ADMIN")]
    public class RoleController : Controller
    {
        private readonly IUserService _userService;

        public RoleController(IServiceFactory serviceFactory)
            : base()
        {
            _userService = serviceFactory.UserService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Json(await _userService.GetRoles());
        }
    }
}