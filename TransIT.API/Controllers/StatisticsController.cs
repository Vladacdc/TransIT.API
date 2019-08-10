using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        [Route("api/v1/[controller]/countmalfunction")]
        public async Task<IActionResult> CountMalfunction(string malfunctionName, string vehicleTypeName)
        {
            try
            {
                var result = await _statisticsService.CountMalfunction(malfunctionName, vehicleTypeName);
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
        [Route("api/v1/[controller]/countmalfunctionsubgroup")]
        public async Task<IActionResult> CountMalfunctionSubgroup(string malfunctionSubgroupName, string vehicleTypeName)
        {
            try
            {
                var result = await _statisticsService.CountMalfunctionSubGroup(malfunctionSubgroupName, vehicleTypeName);
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
        [Route("api/v1/[controller]/countmalfunctiongroup")]
        public async Task<IActionResult> CountMalfunctionGroup(string malfunctionGroupName, string vehicleTypeName)
        {
            try
            {
                var result = await _statisticsService.CountMalfunctionGroup(malfunctionGroupName, vehicleTypeName);
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
        [Route("api/v1/[controller]/malfunctionstatistics")]
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
        [Route("api/v1/[controller]/malfunctiongroupstatistics")]
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
        [Route("api/v1/[controller]/malfunctionsubgroupstatistics")]
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
    }
}
