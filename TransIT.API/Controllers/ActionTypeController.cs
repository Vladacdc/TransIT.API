using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using System.Security.Claims;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,REGISTER,ANALYST")]
    public class ActionTypeController : Controller
    {
        private readonly IActionTypeService _actionTypeService;

        public ActionTypeController(IActionTypeService actionTypeService)
        {
            _actionTypeService = actionTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _actionTypeService.GetRangeAsync(offset, amount);
            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _actionTypeService.GetAsync(id);
            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpGet("/search")]
        public async Task<IActionResult> Get([FromQuery] string search)
        {
            var result = await _actionTypeService.SearchAsync(search);
            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] ActionTypeDTO actionTypeDTO)
        {
            var createdDTO = await _actionTypeService.CreateAsync(actionTypeDTO);

            if (createdDTO != null)
            {
                return CreatedAtAction(nameof(Create), createdDTO);
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] ActionTypeDTO actionTypeDTO)
        {
            actionTypeDTO.Id = id;

            var result = await _actionTypeService.UpdateAsync(actionTypeDTO);

            if (result != null)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _actionTypeService.DeleteAsync(id);
            return NoContent();
        }


    }
}
