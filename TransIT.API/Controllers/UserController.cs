using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.Extensions;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.DependencyInjection;
using TransIT.DAL.Models.Entities;
using Microsoft.AspNetCore.Cors;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ADMIN,ENGINEER")]
    public class UserController : Controller
    { 
        private readonly IUserService _userService;

        public UserController(IUserService userService) 
        {
            _userService = userService;
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(string id, [FromBody] UserDTO obj)
        {
            obj.Id = id;
            var result = await _userService.UpdateAsync(obj);
            return result != null
                ? NoContent()
                : (IActionResult)BadRequest();
        }

        [HttpPut("{id}/password")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordDTO changePassword)
        {
            UserDTO user = await _userService.GetAsync(id);
            var result = await _userService.UpdatePasswordAsync(user, changePassword.OldPassword, changePassword.Password);
           
            return result != null 
                ? NoContent()
                : (IActionResult) BadRequest();
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            switch (User.FindFirst(ROLE.ROLE_SCHEMA)?.Value)
            {
                case ROLE.ADMIN:
                    return Ok(await _userService.GetRangeAsync(offset, amount));
                case ROLE.ENGINEER:
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
            var userCreatedResult = await _userService.CreateAsync(obj);

            return userCreatedResult != null ?
                CreatedAtAction(nameof(Create), userCreatedResult) :
                (IActionResult)BadRequest();
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
