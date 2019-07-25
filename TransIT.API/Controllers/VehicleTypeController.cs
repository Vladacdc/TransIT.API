using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN,ANALYST,ENGINEER")]
    public class VehicleTypeController : DataController<int, VehicleType, VehicleTypeDTO>
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleTypeController(
            IMapper mapper, 
            IVehicleTypeService vehicleTypeService,
            IFilterService<VehicleType> odService
            ) : base(mapper, vehicleTypeService, odService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Create([FromBody] VehicleTypeDTO obj)
        {
            return base.Create(obj);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "ADMIN")]
        public override Task<IActionResult> Update(int id, [FromBody] VehicleTypeDTO obj)
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

