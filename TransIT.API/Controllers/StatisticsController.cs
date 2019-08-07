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

        [HttpPost]

        public async Task<IActionResult> CountMalfunction(string malfunctionName, string vehicleTypeName)
        {
            var result = await _statisticsService.CountMalfunction(malfunctionName, vehicleTypeName);
            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }

        [HttpPost]

        public async Task<IActionResult> CountMalfunctionSubgroup(string malfunctionSubgroupName, string vehicleTypeName)
        {
            var result = await _statisticsService.CountMalfunctionSubGroup(malfunctionSubgroupName, vehicleTypeName);
            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }

        [HttpPost]

        public async Task<IActionResult> CountMalfunctionGroup(string malfunctionGroupName, string vehicleTypeName)
        {
            var result = await _statisticsService.CountMalfunctionGroup(malfunctionGroupName, vehicleTypeName);
            if (result != null)
            {
                return Json(result);
            }

            return BadRequest();
        }
    }
}
