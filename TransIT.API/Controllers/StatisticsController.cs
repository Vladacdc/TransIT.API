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
        public async Task<IActionResult> CountMalfunction(string malfunctionName, string vehicleTypeName, DateTime? startDate, DateTime? endDate)
        {
            int result = await _statisticsService.CountMalfunction(malfunctionName, vehicleTypeName, startDate, endDate);

            return Json(result);
        }

        [HttpGet]
        [Route("countMalfunctionSubgroup")]
        public async Task<IActionResult> CountMalfunctionSubgroup(string malfunctionSubgroupName, string vehicleTypeName, DateTime? startDate, DateTime? endDate)
        {
            int result = await _statisticsService.CountMalfunctionSubGroup(malfunctionSubgroupName, vehicleTypeName, startDate, endDate);

            return Json(result);
        }

        [HttpGet]
        [Route("countMalfunctionGroup")]
        public async Task<IActionResult> CountMalfunctionGroup(string malfunctionGroupName, string vehicleTypeName, DateTime? startDate, DateTime? endDate)
        {
            int result = await _statisticsService.CountMalfunctionGroup(malfunctionGroupName, vehicleTypeName, startDate, endDate);

            return Json(result);
        }

        [HttpGet]
        [Route("malfunctionStatistics")]
        public async Task<IActionResult> GetMalfunctionStatistics(string malfunctionName, DateTime? startDate, DateTime? endDate)
        {
            return Json(await _statisticsService.GetMalfunctionStatistics(malfunctionName, startDate, endDate));
        }

        [HttpGet]
        [Route("malfunctionGroupStatistics")]
        public async Task<IActionResult> GetMalfunctionGroupStatistics(string malfunctionGroupName, DateTime? startDate, DateTime? endDate)
        {
            return Json(await _statisticsService.GetMalfunctionGroupStatistics(malfunctionGroupName, startDate, endDate));
        }

        [HttpGet]
        [Route("malfunctionSubgroupStatistics")]
        public async Task<IActionResult> GetMalfunctionSubGroupStatistics(string malfunctionSubGroupName, DateTime? startDate, DateTime? endDate)
        {
            return Json(await _statisticsService.GetMalfunctionSubGroupStatistics(malfunctionSubGroupName, startDate, endDate));
        }

        [HttpGet]
        [Route("allMalfunctionsStatistics")]
        public async Task<IActionResult> GetAllMalfunctionsStatistics(string malfunctionSubgroupName, DateTime? startDate, DateTime? endDate)
        {
            return Json(await _statisticsService.GetAllMalfunctionsStatistics(startDate, endDate, malfunctionSubgroupName));
        }

        [HttpGet]
        [Route("allMalfunctionGroupsStatistics")]
        public async Task<IActionResult> GetAllMalfunctionGroupsStatistics(DateTime? startDate, DateTime? endDate)
        {
            return Json(await _statisticsService.GetAllGroupsStatistics(startDate, endDate));
        }

        [HttpGet]
        [Route("allMalfunctionSubgroupsStatistics")]
        public async Task<IActionResult> GetAllMalfunctionSubgroupsStatistics(string malfunctionGroupName, DateTime? startDate, DateTime? endDate)
        {
            return Json(await _statisticsService.GetAllSubgroupsStatistics(startDate, endDate, malfunctionGroupName));
        }
    }
}
