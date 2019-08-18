using System;
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
            try
            {
                obj.Id = id;
                var result = await _userService.UpdateAsync(obj);
                return result != null
                    ? Ok(result)
                    : (IActionResult)BadRequest("User doesn't exist");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPut("{id}/password")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordDTO changePassword)
        {
            try
            {
                var user = await _userService.GetAsync(id);
                var result = await _userService.UpdatePasswordAsync(user, changePassword.Password);

                return result != null
                    ? NoContent()
                    : null;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            throw new Exception("Hello from User controller");
            try
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
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] UserDTO obj)
        {
            try
            {
                var userCreatedResult = await _userService.CreateAsync(obj);

                return userCreatedResult != null
                    ? CreatedAtAction(nameof(Create), userCreatedResult)
                    : null;
            }
            catch (Exception e)
            {
                throw e;
            }
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