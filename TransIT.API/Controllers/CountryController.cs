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
    [Authorize(Roles = "ADMIN,ENGINEER,REGISTER,ANALYST")]
    public class CountryController : FilterController<CountryDTO>
    {
        private readonly ICountryService _countryService;

        public CountryController(IServiceFactory serviceFactory, IFilterServiceFactory filterServiceFactory)
            : base(filterServiceFactory)
        {
            _countryService = serviceFactory.CountryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] uint offset = 0, uint amount = 1000)
        {
            var result = await _countryService.GetRangeAsync(offset, amount);
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
            var result = await _countryService.GetAsync(id);
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
            var result = await _countryService.SearchAsync(search);
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
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([FromBody] CountryDTO countryDTO)
        {
            var createdDTO = await _countryService.CreateAsync(countryDTO);

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
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Update(int id, [FromBody] CountryDTO countryDTO)
        {
            countryDTO.Id = id;

            var result = await _countryService.UpdateAsync(countryDTO);

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
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(int id)
        {
            await _countryService.DeleteAsync(id);
            return NoContent();
        }
    }
}