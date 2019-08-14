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
        [Route("countmalfunction")]
        public async Task<IActionResult> CountMalfunction(string malfunctionName, string vehicleTypeName)
        {
            try
            {
                int result = await _statisticsService.CountMalfunction(malfunctionName, vehicleTypeName);

                return Json(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("countmalfunctionsubgroup")]
        public async Task<IActionResult> CountMalfunctionSubgroup(string malfunctionSubgroupName, string vehicleTypeName)
        {
            try
            {
                var result = await _statisticsService.CountMalfunctionSubGroup(malfunctionSubgroupName, vehicleTypeName);

                return Json(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("countmalfunctiongroup")]
        public async Task<IActionResult> CountMalfunctionGroup(string malfunctionGroupName, string vehicleTypeName)
        {
            try
            {
                var result = await _statisticsService.CountMalfunctionGroup(malfunctionGroupName, vehicleTypeName);

                return Json(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("malfunctionstatistics")]
        public async Task<IActionResult> GetMalfunctionStatistics(string malfunctionName)
        {
            try
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
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("malfunctiongroupstatistics")]
        public async Task<IActionResult> GetMalfunctionGroupStatistics(string malfunctionGroupName)
        {
            try
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
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("malfunctionsubgroupstatistics")]
        public async Task<IActionResult> GetMalfunctionSubGroupStatistics(string malfunctionSubGroupName)
        {
            try
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
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("allMalfunctionsStatistics")]
        public async Task<IActionResult> GetAllMalfunctionsStatistics(string malfunctionSubgroupName)
        {
            try
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
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("AllMalfunctionGroupsStatistics")]
        public async Task<IActionResult> GetAllMalfunctionGroupsStatistics()
        {
            try
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
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet]
        [Route("AllMalfunctionSubgroupsStatistics")]
        public async Task<IActionResult> GetAllMalfunctionSubgroupsStatistics(string malfunctionGroupName)
        {
            try
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
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
    }
}
