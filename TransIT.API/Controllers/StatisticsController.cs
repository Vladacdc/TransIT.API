using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factories;
using TransIT.BLL.Services.Interfaces;

namespace TransIT.API.Controllers
{
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IServiceFactory serviceFactory)
        {
            _statisticsService = serviceFactory.StatisticService;
        }

        [HttpGet]

        public async Task<IActionResult> Get(MalfunctionDTO malfunctionDto,VehicleTypeDTO vehicleTypeDto)
        {
            var result = await _statisticsService.CountMalfunction(malfunctionDto,vehicleTypeDto);
            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }

        [HttpGet]

        public async Task<IActionResult> Get(MalfunctionSubgroupDTO malfunctionSubgroupDto, VehicleTypeDTO vehicleTypeDto)
        {
            var result = await _statisticsService.CountMalfunctionSubGroup(malfunctionSubgroupDto, vehicleTypeDto);
            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }

        [HttpGet]

        public async Task<IActionResult> Get(MalfunctionGroupDTO malfunctionGroupDto, VehicleTypeDTO vehicleTypeDto)
        {
            var result = await _statisticsService.CountMalfunctionGroup(malfunctionGroupDto, vehicleTypeDto);
            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }
    }
}
