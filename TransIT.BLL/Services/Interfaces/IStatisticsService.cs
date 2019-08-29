using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;

namespace TransIT.BLL.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<int> CountMalfunction(string malfunctionName, string vehicleTypeName, DateTime startDate, DateTime endDate);

        Task<int> CountMalfunctionSubGroup(string malfunctionSubgroupName, string vehicleTypeName, DateTime startDate, DateTime endDate);

        Task<int> CountMalfunctionGroup(string malfunctionGroupName, string vehicleTypeName, DateTime startDate, DateTime endDate);

        Task<List<int>> GetMalfunctionStatistics(string malfunctionName, DateTime startDate, DateTime endDate);

        Task<List<int>> GetMalfunctionSubGroupStatistics(string malfunctionSubgroupName, DateTime startDate, DateTime endDate);

        Task<List<int>> GetMalfunctionGroupStatistics(string malfunctionGroupName, DateTime startDate, DateTime endDate);

        Task<List<StatisticsDTO>> GetAllGroupsStatistics(DateTime startDate, DateTime endDate);

        Task<List<StatisticsDTO>> GetAllSubgroupsStatistics(DateTime startDate, DateTime endDatestring, string groupName = null);

        Task<List<StatisticsDTO>> GetAllMalfunctionsStatistics(DateTime startDate, DateTime endDate, string subgroupName = null);
    }
}
