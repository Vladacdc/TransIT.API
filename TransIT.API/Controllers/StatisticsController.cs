using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransIT.BLL.Factories;
using TransIT.BLL.Services.Interfaces;

namespace TransIT.API.Controllers
{
    [ApiController]
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    [Authorize(Roles = "ANALYST")]
    public class StatisticsController : Controller
    {
        private readonly IStatisticsService _statisticsService;

        public StatisticsController(IServiceFactory serviceFactory)
        {
            _statisticsService = serviceFactory.StatisticService;
        }

        [HttpGet]
        [Route("countMalfunction")]
        public async Task<IActionResult> CountMalfunction(string malfunctionName, string vehicleTypeName)
        {
            int result = await _statisticsService.CountMalfunction(malfunctionName, vehicleTypeName);

            return Json(result);
        }

        [HttpGet]
        [Route("countMalfunctionSubgroup")]
        public async Task<IActionResult> CountMalfunctionSubgroup(string malfunctionSubgroupName, string vehicleTypeName)
        {
            int result = await _statisticsService.CountMalfunctionSubGroup(malfunctionSubgroupName, vehicleTypeName);

            return Json(result);
        }

        [HttpGet]
        [Route("countMalfunctionGroup")]
        public async Task<IActionResult> CountMalfunctionGroup(string malfunctionGroupName, string vehicleTypeName)
        {
            int result = await _statisticsService.CountMalfunctionGroup(malfunctionGroupName, vehicleTypeName);

            return Json(result);
        }

        [HttpGet]
        [Route("malfunctionStatistics")]
        public async Task<IActionResult> GetMalfunctionStatistics(string malfunctionName)
        {
            var result = await _statisticsService.GetMalfunctionStatistics(malfunctionName);

            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("malfunctionGroupStatistics")]
        public async Task<IActionResult> GetMalfunctionGroupStatistics(string malfunctionGroupName)
        {
            var result = await _statisticsService.GetMalfunctionGroupStatistics(malfunctionGroupName);

            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("malfunctionSubgroupStatistics")]
        public async Task<IActionResult> GetMalfunctionSubGroupStatistics(string malfunctionSubGroupName)
        {
            var result = await _statisticsService.GetMalfunctionSubGroupStatistics(malfunctionSubGroupName);

            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("allMalfunctionsStatistics")]
        public async Task<IActionResult> GetAllMalfunctionsStatistics(string malfunctionSubgroupName)
        {
            var result = await _statisticsService.GetAllMalfunctionsStatistics(malfunctionSubgroupName);

            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("allMalfunctionGroupsStatistics")]
        public async Task<IActionResult> GetAllMalfunctionGroupsStatistics()
        {
            var result = await _statisticsService.GetAllGroupsStatistics();

            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return null;
            }
        }

        [HttpGet]
        [Route("allMalfunctionSubgroupsStatistics")]
        public async Task<IActionResult> GetAllMalfunctionSubgroupsStatistics(string malfunctionGroupName)
        {
            var result = await _statisticsService.GetAllSubgroupsStatistics(malfunctionGroupName);

            if (result != null)
            {
                return Json(result);
            }
            else
            {
                return null;
            }
        }
    }
}
