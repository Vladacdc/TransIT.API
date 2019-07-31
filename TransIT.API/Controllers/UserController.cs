using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.Extensions;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factory;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ADMIN,ENGINEER")]
    public class UserController : FilterController<UserDTO>
    {
        private readonly IServiceFactory _serviceFactory;

        public UserController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(string id, [FromBody] UserDTO obj)
        {
            obj.Id = id;
            var result = await _serviceFactory.UserService.UpdateAsync(obj);
            return result != null
                ? NoContent()
                : (IActionResult)BadRequest();
        }

        [HttpPut("{id}/password")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordDTO changePassword)
        {
            var user = await _serviceFactory.UserService.GetAsync(id);
            var result = await _serviceFactory.UserService.UpdatePasswordAsync(user, changePassword.OldPassword, changePassword.Password);

            return result != null
                ? NoContent()
                : (IActionResult)BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            switch (User.FindFirst(RoleNames.Schema)?.Value)
            {
                case RoleNames.Admin:
                    return Ok(await _serviceFactory.UserService.GetRangeAsync(offset, amount));
                case RoleNames.Engineer:
                    var result = await _serviceFactory.UserService.GetAssignees(offset, amount);
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
            var userCreatedResult = await _serviceFactory.UserService.CreateAsync(obj);

            return userCreatedResult != null
                ? CreatedAtAction(nameof(Create), userCreatedResult)
                : (IActionResult)BadRequest();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(string id)
        {
            await _serviceFactory.UserService.DeleteAsync(id);
            return NoContent();
        }
    }
}