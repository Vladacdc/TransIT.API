using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factories;
using TransIT.BLL.Services.Interfaces;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ENGINEER,REGISTER,ANALYST")]
    public class BillController : FilterController<BillDTO>
    {
        private readonly IBillService _billService;

        public BillController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _billService = serviceFactory.BillService;
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
                return null;
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
                return null;
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
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BillDTO billDTO)
        {
            var createdDTO = await _billService.CreateAsync(billDTO);

            if (createdDTO != null)
            {
                return CreatedAtAction(nameof(Create), createdDTO);
            }
            else
            {
                return null;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BillDTO billDTO)
        {
            billDTO.Id = id;

            var result = await _billService.UpdateAsync(billDTO);

            if (result != null)
            {
                return NoContent();
            }
            else
            {
                return null;
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