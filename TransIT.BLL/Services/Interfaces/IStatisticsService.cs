using System.Collections.Generic;
using System.Threading.Tasks;

namespace TransIT.BLL.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<int> CountMalfunction(string malfunctionName, string vehicleTypeName);

        Task<int> CountMalfunctionSubGroup(string malfunctionSubgroupName, string vehicleTypeName);

        Task<int> CountMalfunctionGroup(string malfunctionGroupName, string vehicleTypeName);

        Task<List<int>> GetMalfunctionStatistics(string malfunctionName);

        Task<List<int>> GetMalfunctionSubGroupStatistics(string malfunctionSubgroupName);

        Task<List<int>> GetMalfunctionGroupStatistics(string malfunctionGroupName);
    }
}
