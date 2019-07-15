using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.Extensions;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER")]
    public class UserController : DataController<User, UserDTO>
    {
        private readonly IUserService _userService;
        
        public UserController(
            IMapper mapper, 
            IUserService userService,
            IFilterService<User> odService
            ) : base(mapper, userService, odService)
        {
            _userService = userService;
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Update(int id, [FromBody] UserDTO obj)
        {
            obj.Password = null;
            return base.Update(id, obj);
        }

        [HttpPut("{id}/password")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePasswordDTO changePassword)
        {
            var user = await _userService.GetAsync(id);
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            user.ModId = adminId;
            return await _userService.UpdatePasswordAsync(user, changePassword.Password) != null 
                ? NoContent()
                : (IActionResult) BadRequest();
        }
        
        [HttpGet]
        public override async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            switch (User.FindFirst(ROLE.ROLE_SCHEMA)?.Value)
            {
                case ROLE.ADMIN:
                    return await base.Get(offset, amount);
                case ROLE.ENGINEER:
                    var result = await _userService.GetAssignees(offset, amount);
                    return result != null
                        ? Json(_mapper.Map<IEnumerable<UserDTO>>(result))
                        : (IActionResult) BadRequest();
                default:
                    return BadRequest();
            }
        }

        [HttpGet("/search")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Get([FromQuery] string search)
        {
            return base.Get(search);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Create([FromBody] UserDTO obj)
        {
            return base.Create(obj);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
