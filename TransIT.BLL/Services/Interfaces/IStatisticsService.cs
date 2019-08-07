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
        Task<int> CountMalfunction(MalfunctionDTO malfunctionDto, VehicleTypeDTO vehicleTypeDto);

        Task<int> CountMalfunctionSubGroup(MalfunctionSubgroupDTO malfunctionSubgroupDto, VehicleTypeDTO vehicleTypeDto);

        Task<int> CountMalfunctionGroup(MalfunctionGroupDTO malfunctionGroupDto, VehicleTypeDTO vehicleTypeDto);
    }
}
