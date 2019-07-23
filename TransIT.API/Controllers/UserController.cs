using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TransIT.API.Extensions;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER")]
    public class UserController : DataController<string, User, UserDTO>
    {
        private readonly IUserService _userService;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<Role> _roleManager;
        

        public UserController(
            IMapper mapper, 
            IUserService userService,
            IFilterService<string, User> odService,
            UserManager<User> userManager,
            RoleManager<Role> roleManager
            ) : base(mapper, userService, odService)
        {
            _userService = userService;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Update(string id, [FromBody] UserDTO obj)
        {
            obj.Password = null;
            return base.Update(id, obj);
        }

        [HttpPut("{id}/password")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> ChangePassword(string id, [FromBody] ChangePasswordDTO changePassword)
        {
            var user = await _userService.GetAsync(id);
            var adminId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            throw new System.NotImplementedException(nameof(adminId));
            //user.ModifiedById = adminId;
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
                        : (IActionResult)BadRequest();
                default:
                    return BadRequest();
            }
        }

        //[HttpGet("/search")]
        //[Authorize(Roles = "ADMIN")]
        //public override Task<IActionResult> Get([FromQuery] string search)
        //{
        //    return base.Get(search);
        //}

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override async Task<IActionResult> Create([FromBody] UserDTO obj)
        {
            var user = _mapper.Map<User>(obj);
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            user.ModifiedById = userId;
            user.CreatedById = userId;

            var result = await _userManager.CreateAsync(user, obj.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, obj.Role.Name);
                return CreatedAtAction(nameof(Create), _mapper.Map<UserDTO>(user));
            }
            else
            {
                return (IActionResult)BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Delete(string id)
        {
            return base.Delete(id);
        }
    }
}
