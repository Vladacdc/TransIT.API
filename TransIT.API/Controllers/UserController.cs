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
namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER")]
    public class UserController: Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IFilterService<User> _odService;

        private readonly UserManager<User> _userManager;

        private readonly RoleManager<Role> _roleManager;

        private readonly IUser _user;
        private readonly DataController<User, UserDTO> _dataController;


        public UserController(
            IMapper mapper, 
            IUserService userService,
            IFilterService<User> odService,
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IUser user,
            DataController<User, UserDTO> dataController
            )
        {
            _mapper = mapper;
            _userService = userService;
            _odService = odService;
            _userManager = userManager;
            _roleManager = roleManager;
            _user = user;
            _dataController = dataController;
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IdentityResult> Update(string id, [FromBody] UserDTO obj)
        {
            User updatedUser = await _userManager.FindByIdAsync(id);
            var adminId = _user.CurrentUserId;

            updatedUser.FirstName = obj.FirstName;
            updatedUser.LastName = obj.LastName;
            updatedUser.MiddleName = obj.MiddleName;
            updatedUser.UserName = obj.UserName;
            updatedUser.Email = obj.Email;
            updatedUser.PhoneNumber = obj.PhoneNumber;

            await _userManager.RemoveFromRoleAsync(updatedUser, _userManager.GetRolesAsync(updatedUser).Result.FirstOrDefault());
            await _userManager.AddToRoleAsync(updatedUser, obj.Role.Name);

            updatedUser.UpdatedById = adminId;
            
            var result = await _userManager.UpdateAsync(updatedUser);
            return result;
        }

        [HttpPut("{id}/password")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IdentityResult> ChangePassword(string id, [FromBody] ChangePasswordDTO changePassword)
        {
            var user = await _userManager.FindByIdAsync(id);
            var adminId = _user.CurrentUserId;
            
            user.UpdatedById = adminId;
            await _userManager.UpdateAsync(user);
            var result = await _userService.UpdatePasswordAsync(user, changePassword.Password);
            return result;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            switch (User.FindFirst(ROLE.ROLE_SCHEMA)?.Value)
            {
                case ROLE.ADMIN:
                    return await _dataController.Get(offset, amount);
                case ROLE.ENGINEER:
                    var result = await _userService.GetAssignees(offset, amount);
                    return result != null
                        ? Json(_mapper.Map<IEnumerable<UserDTO>>(result))
                        : (IActionResult)BadRequest();
                default:
                    return StatusCode(StatusCodes.Status500InternalServerError);
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
        public async Task<IActionResult> Create([FromBody] UserDTO obj)
        {
            var user = _mapper.Map<User>(obj);
            var adminId = _user.CurrentUserId;

            user.UpdatedById = adminId;
            user.CreatedById = adminId;

            var userCreatedResult = await _userManager.CreateAsync(user, obj.Password);

            if (userCreatedResult.Succeeded)
            {
                var roleCreatedResult = await _userManager.AddToRoleAsync(user, obj.Role.Name);
                if (roleCreatedResult.Succeeded)
                {
                    return StatusCode(StatusCodes.Status201Created);
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IdentityResult> Delete(string id)
        {
           var result = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));
           return result;
        }
    }
}
