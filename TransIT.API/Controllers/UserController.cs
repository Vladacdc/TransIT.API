using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.Extensions;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factories;
using TransIT.BLL.Services.Interfaces;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ADMIN,ENGINEER")]
    public class UserController : FilterController<UserDTO>
    {
        private readonly IUserService _userService;

        public UserController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _userService = serviceFactory.UserService;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(string id, [FromBody] UserDTO obj)
        {
            obj.Id = id;
            var result = await _userService.UpdateAsync(obj);
            return result != null
                ? Ok(result)
                : (IActionResult)BadRequest("User doesn't exist");
        }

        [HttpPut("{id}/password")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordDTO changePassword)
        {
            var user = await _userService.GetAsync(id);
            var result = await _userService.UpdatePasswordAsync(user, changePassword.Password);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            switch (User.FindFirst(RoleNames.Schema)?.Value)
            {
                case RoleNames.Admin:
                    return Ok(await _userService.GetRangeAsync(offset, amount));
                case RoleNames.Engineer:
                    var result = await _userService.GetAssignees(offset, amount);
                    return result != null
                        ? Ok(result)
                        : (IActionResult)BadRequest();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] UserDTO obj)
        {
            return CreatedAtAction(nameof(Create), await _userService.CreateAsync(obj));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
