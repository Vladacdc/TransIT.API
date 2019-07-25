using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,ANALYST")]
    public class MalfunctionGroupController : DataController<MalfunctionGroup, MalfunctionGroupDTO>
    {
        private readonly IMalfunctionGroupService _malfunctionGroupService;

        public MalfunctionGroupController(
            IMapper mapper, 
            IMalfunctionGroupService malfunctionGroupService,
            IFilterService<MalfunctionGroup> odService
            ) : base(mapper, malfunctionGroupService, odService)
        {
            _malfunctionGroupService = malfunctionGroupService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Create([FromBody] MalfunctionGroupDTO obj)
        {
            return base.Create(obj);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Update(int id, [FromBody] MalfunctionGroupDTO obj)
        {
            return base.Update(id, obj);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Delete(int id)
        {
            return base.Delete(id);
        }
    }
}
