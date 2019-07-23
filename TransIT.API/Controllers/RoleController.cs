using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class RoleController : DataController<string, Role, RoleDTO>
    {
        private readonly IRoleService _roleService;
        
        public RoleController(
            IMapper mapper, 
            IRoleService roleService,
            IFilterService<string, Role> odService
            ) : base(mapper, roleService, odService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public override Task<IActionResult> Create([FromBody] RoleDTO obj)
        {
            return Task.FromResult(StatusCode(501) as IActionResult);
        }
        
        [HttpPut("{id}")]
        public override Task<IActionResult> Update(string id, [FromBody] RoleDTO obj)
        {
            return Task.FromResult(StatusCode(501) as IActionResult);
        }

        [HttpDelete("{id}")]
        public override Task<IActionResult> Delete(string id)
        {   
            return Task.FromResult(StatusCode(501) as IActionResult);
        }
    }
}
