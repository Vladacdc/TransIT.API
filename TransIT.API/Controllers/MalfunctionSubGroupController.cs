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
    public class MalfunctionSubGroupController : DataController<MalfunctionSubgroup, MalfunctionSubgroupDTO>
    {
        private readonly IMalfunctionSubgroupService _malfunctionSubgroupService;

        public MalfunctionSubGroupController(
            IMapper mapper, 
            IMalfunctionSubgroupService malfunctionSubgroupService,
            IFilterService<MalfunctionSubgroup> odService
            ) : base(mapper, malfunctionSubgroupService, odService)
        {
            _malfunctionSubgroupService = malfunctionSubgroupService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Create([FromBody] MalfunctionSubgroupDTO obj)
        {
            return base.Create(obj);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Update(int id, [FromBody] MalfunctionSubgroupDTO obj)
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
