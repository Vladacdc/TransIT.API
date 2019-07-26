using Microsoft.AspNetCore.Authorization;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Security.Claims;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ENGINEER,REGISTER,ANALYST")]
    public class BillController : Controller
    {
        private readonly IBillService _billService;
        
        public BillController(IBillService billService)
        {
            _billService = billService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _billService.GetRangeAsync(offset, amount);
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
            var result = await _billService.GetAsync(id);
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
            var result = await _billService.SearchAsync(search);
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
        public async Task<IActionResult> Create([FromBody] BillDTO billDTO)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var createdDTO = await _billService.CreateAsync(billDTO, userId);

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
        public async Task<IActionResult> Update(int id, [FromBody] BillDTO billDTO)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            billDTO.Id = id;

            var result = await _billService.UpdateAsync(billDTO, userId);

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
        public async Task<IActionResult> Delete(int id)
        {
            await _billService.DeleteAsync(id);
            return NoContent();
        }

    }
}
