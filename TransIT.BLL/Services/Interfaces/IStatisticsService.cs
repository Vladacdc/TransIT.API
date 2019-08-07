using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models.Entities;

namespace TransIT.BLL.Services.Interfaces
{
    public interface IStatisticsService
    {
        Task<int> CountMalfunction(string malfunctionName, string vehicleTypeName);

        Task<int> CountMalfunctionSubGroup(string malfunctionSubgroupName, string vehicleTypeName);

        Task<int> CountMalfunctionGroup(string malfunctionGroupName, string vehicleTypeName);
    }
}
