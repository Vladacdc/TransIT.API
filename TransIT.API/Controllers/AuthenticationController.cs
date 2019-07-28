using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;

namespace TransIT.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationService _authenticationService;
        
        public AuthenticationController(IAuthenticationService authenticationService) =>
            _authenticationService = authenticationService;
        
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginDTO credentials)
        {
            var result = await _authenticationService.SignInAsync(credentials);
            return result != null 
                ? Json(result)
                : (IActionResult) BadRequest();
        }

        [HttpPost]
        [Route("refreshtoken")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenDTO token)
        {
            var result = await _authenticationService.TokenAsync(token);
            return result != null
                ? Json(result)
                : (IActionResult) Forbid();
        }
    }
}
