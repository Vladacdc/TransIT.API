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
    [Authorize(Roles = "ADMIN,ENGINEER,REGISTER,ANALYST")]
    public class ActionTypeController : DataController<int, ActionType, ActionTypeDTO>
    {
        private readonly IActionTypeService _actionTypeService;

        public ActionTypeController(
            IMapper mapper,
            IActionTypeService actionType,
            IFilterService<int, ActionType> odService
            ) : base(mapper, actionType, odService)
        {
            _actionTypeService = actionType;
        }
        
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Create([FromBody] ActionTypeDTO obj)
        {
            return base.Create(obj);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Update(int id, [FromBody] ActionTypeDTO obj)
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
