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
    [Authorize(Roles = "ADMIN,WORKER,ENGINEER,REGISTER,ANALYST")]
    public class StateController : DataController<int, State, StateDTO>
    {
        private readonly IStateService _stateService;
        
        public StateController(
            IMapper mapper,
            IStateService stateService,
            IFilterService<int, State> odService
            ) : base(mapper, stateService, odService)
        {
            _stateService = stateService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Create([FromBody] StateDTO obj)
        {
            return base.Create(obj);
        }
        
        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Update(int id, [FromBody] StateDTO obj)
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
