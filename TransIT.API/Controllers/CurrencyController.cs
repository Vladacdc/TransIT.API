using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services;
using TransIT.BLL.Services.Interfaces;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;
using System.Security.Claims;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ENGINEER,REGISTER,ANALYST")]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _currencyService.GetRangeAsync(offset, amount);
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
            var result = await _currencyService.GetAsync(id);
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
            var result = await _currencyService.SearchAsync(search);
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
        public async Task<IActionResult> Create([FromBody] CurrencyDTO currencyDTO)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var createdDTO = await _currencyService.CreateAsync(userId, currencyDTO);

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
        public async Task<IActionResult> Update(int id, [FromBody] CurrencyDTO currencyDTO)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            currencyDTO.Id = id;

            var result = await _currencyService.UpdateAsync(userId, currencyDTO);

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
            await _currencyService.DeleteAsync(id);
            return NoContent();
        }
    }
}
