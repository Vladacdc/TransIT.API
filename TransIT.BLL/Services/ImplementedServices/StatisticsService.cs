using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        public StatisticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> CountMalfunction(string malfunctionName, string vehicleTypeName, DateTime startDate, DateTime endDate)
        {
            throw new Exception("TODO");

            var issues = await _unitOfWork.IssueRepository.GetAllAsync(
                i =>i.Vehicle.VehicleType.Name == vehicleTypeName && i.Malfunction.Name==malfunctionName);
            return issues.Count();
        }

        public async Task<int> CountMalfunctionSubGroup(string malfunctionSubgroupName, string vehicleTypeName, DateTime startDate, DateTime endDate)
        {
            int count = 0;
            var malfunctions = 
                await _unitOfWork.MalfunctionRepository.GetAllAsync(
                    m => m.MalfunctionSubgroup.Name == malfunctionSubgroupName);

            if (malfunctions != null)
            {
                foreach (var i in malfunctions)
                {
                    count += await CountMalfunction(i.Name, vehicleTypeName, startDate, endDate);
                }
            }

            return count;
        }

        public async Task<int> CountMalfunctionGroup(string malfunctionGroupName, string vehicleTypeName, DateTime startDate, DateTime endDate)
        {
            int count = 0;
            var malfunctionSubgroups = 
                await _unitOfWork.MalfunctionSubgroupRepository.GetAllAsync(
                    sg => sg.MalfunctionGroup.Name == malfunctionGroupName);

            if (malfunctionSubgroups != null)
            {
                foreach (var i in malfunctionSubgroups)
                {
                    count += await CountMalfunctionSubGroup(i.Name, vehicleTypeName, startDate, endDate);
                }
            }

            return count;
        }

        public async Task<List<int>> GetMalfunctionStatistics(string malfunctionName, DateTime startDate, DateTime endDate)
        {
            var vehicleTypes = await _unitOfWork.VehicleTypeRepository.GetAllAsync();
            List<int> result = new List<int>();

            foreach (VehicleType vehicleType in vehicleTypes)
            {
                result.Add(await CountMalfunction(malfunctionName, vehicleType.Name, startDate, endDate));
            }

            return result;
        }

        public async Task<List<int>> GetMalfunctionSubGroupStatistics(string malfunctionSubGroupName, DateTime startDate, DateTime endDate)
        {
            var vehicleTypes = await _unitOfWork.VehicleTypeRepository.GetAllAsync();
            List<int> result = new List<int>();

            foreach (VehicleType vehicleType in vehicleTypes)
            {
                result.Add(await CountMalfunctionSubGroup(malfunctionSubGroupName, vehicleType.Name, startDate, endDate));
            }

            return result;
        }

        public async Task<List<int>> GetMalfunctionGroupStatistics(string malfunctionGroupName, DateTime startDate, DateTime endDate)
        {
            var vehicleTypes = await _unitOfWork.VehicleTypeRepository.GetAllAsync();
            List<int> result = new List<int>();

            foreach (VehicleType vehicleType in vehicleTypes)
            {
                result.Add(await CountMalfunctionGroup(malfunctionGroupName, vehicleType.Name, startDate, endDate));
            }

            return result;
        }

        public async Task<List<StatisticsDTO>> GetAllGroupsStatistics(DateTime startDate, DateTime endDate)
        {
            var malfunctionGroups = await _unitOfWork.MalfunctionGroupRepository.GetAllAsync();
            List<StatisticsDTO> result = new List<StatisticsDTO>();

            foreach (MalfunctionGroup group in malfunctionGroups)
            {
                result.Add(new StatisticsDTO
                {
                    FieldName = group.Name,
                    Statistics = await GetMalfunctionGroupStatistics(group.Name, startDate, endDate)
                });
            }

            return result;
        }

        public async Task<List<StatisticsDTO>> GetAllSubgroupsStatistics(DateTime startDate, DateTime endDate, string groupName = null)
        {
            List<StatisticsDTO> result = new List<StatisticsDTO>();
            IEnumerable<MalfunctionSubgroup> malfunctionSubgroups;

            if (groupName == null)
            {
                malfunctionSubgroups = await _unitOfWork.MalfunctionSubgroupRepository.GetAllAsync();
            }
            else
            {
                malfunctionSubgroups = await _unitOfWork.MalfunctionSubgroupRepository.GetAllAsync(
                    ms => ms.MalfunctionGroup.Name == groupName);
            }

            foreach (MalfunctionSubgroup subgroup in malfunctionSubgroups)
            {
                result.Add(new StatisticsDTO
                {
                    FieldName = subgroup.Name,
                    Statistics = await GetMalfunctionSubGroupStatistics(subgroup.Name, startDate, endDate)
                });
            }

            return result;
        }

        public async Task<List<StatisticsDTO>> GetAllMalfunctionsStatistics(DateTime startDate, DateTime endDate, string subgroupName = null)
        {
            List<StatisticsDTO> result = new List<StatisticsDTO>();
            IEnumerable<Malfunction> malfunctions;

            if (subgroupName == null)
            {
                malfunctions = await _unitOfWork.MalfunctionRepository.GetAllAsync();
            }
            else
            {
                malfunctions = await _unitOfWork.MalfunctionRepository.GetAllAsync(
                    m => m.MalfunctionSubgroup.Name == subgroupName);
            }

            foreach (Malfunction malfunction in malfunctions)
            {
                result.Add(new StatisticsDTO
                {
                    FieldName = malfunction.Name,
                    Statistics = await GetMalfunctionStatistics(malfunction.Name, startDate, endDate)
                });
            }

            return result;
        }
    }
}
