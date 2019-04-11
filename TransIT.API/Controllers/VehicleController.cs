﻿using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Services.InterfacesRepositories;
using TransIT.DAL.Models.DTOs;
using TransIT.DAL.Models.Entities;


namespace TransIT.API.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class VehicleController : DataController<Vehicle, VehicleDTO>
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IMapper mapper, IVehicleService vehicleService) : base(mapper, vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST,WORKER")]
        public override Task<IActionResult> Get(int id)
        {
            return base.Get(id);
        }

        [HttpGet("/search")]
        [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST,WORKER")]
        public override Task<IActionResult> Get([FromQuery] string search)
        {
            return base.Get(search);
        }

        [HttpGet]
        [Authorize(Roles = "ADMIN,ENGINEER,CUSTOMER,ANALYST,WORKER")]
        public override Task<IActionResult> Get([FromQuery] uint offset, uint amount)
        {
            return base.Get(offset, amount);
        }
    }
}