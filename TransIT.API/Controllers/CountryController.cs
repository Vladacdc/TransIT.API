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
    public class CountryController : DataController<int, Country, CountryDTO>
    {
        private readonly ICountryService _countryService;

        public CountryController(
            IMapper mapper,
            ICountryService countryService,
            IFilterService<int, Country> odService
            ) : base(mapper, countryService, odService)
        {
            _countryService = countryService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Create([FromBody] CountryDTO obj)
        {
            return base.Create(obj);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Update(int id, [FromBody] CountryDTO obj)
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
