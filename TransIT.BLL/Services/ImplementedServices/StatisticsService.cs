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
        private DateTime StartDate;

        private DateTime EndDate;

        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        public StatisticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void SetDateRange(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public async Task<int> CountMalfunction(string malfunctionName, string vehicleTypeName)
        {
            var issues = await _unitOfWork.IssueRepository.GetAllAsync(i =>
                i.Vehicle.VehicleType.Name == vehicleTypeName &&
                i.Malfunction.Name == malfunctionName &&
                (StartDate == null ? true : i.CreatedDate >= StartDate) &&
                (EndDate == null ? true : i.CreatedDate <= EndDate));
            return issues.Count();
        }

        public async Task<int> CountMalfunctionSubGroup(string malfunctionSubgroupName, string vehicleTypeName)
        {
            int count = 0;
            var malfunctions = 
                await _unitOfWork.MalfunctionRepository.GetAllAsync(
                    m => m.MalfunctionSubgroup.Name == malfunctionSubgroupName);

            if (malfunctions != null)
            {
                foreach (var i in malfunctions)
                {
                    count += await CountMalfunction(i.Name, vehicleTypeName);
                }
            }

            return count;
        }

        public async Task<int> CountMalfunctionGroup(string malfunctionGroupName, string vehicleTypeName)
        {
            int count = 0;
            var malfunctionSubgroups = 
                await _unitOfWork.MalfunctionSubgroupRepository.GetAllAsync(
                    sg => sg.MalfunctionGroup.Name == malfunctionGroupName);

            if (malfunctionSubgroups != null)
            {
                foreach (var i in malfunctionSubgroups)
                {
                    count += await CountMalfunctionSubGroup(i.Name, vehicleTypeName);
                }
            }

            return count;
        }

        public async Task<List<int>> GetMalfunctionStatistics(string malfunctionName)
        {
            var vehicleTypes = await _unitOfWork.VehicleTypeRepository.GetAllAsync();
            List<int> result = new List<int>();

            foreach (VehicleType vehicleType in vehicleTypes)
            {
                result.Add(await CountMalfunction(malfunctionName, vehicleType.Name));
            }

            return result;
        }

        public async Task<List<int>> GetMalfunctionSubGroupStatistics(string malfunctionSubGroupName)
        {
            var vehicleTypes = await _unitOfWork.VehicleTypeRepository.GetAllAsync();
            List<int> result = new List<int>();

            foreach (VehicleType vehicleType in vehicleTypes)
            {
                result.Add(await CountMalfunctionSubGroup(malfunctionSubGroupName, vehicleType.Name));
            }

            return result;
        }

        public async Task<List<int>> GetMalfunctionGroupStatistics(string malfunctionGroupName)
        {
            var vehicleTypes = await _unitOfWork.VehicleTypeRepository.GetAllAsync();
            List<int> result = new List<int>();

            foreach (VehicleType vehicleType in vehicleTypes)
            {
                result.Add(await CountMalfunctionGroup(malfunctionGroupName, vehicleType.Name));
            }

            return result;
        }

        public async Task<List<StatisticsDTO>> GetAllGroupsStatistics()
        {
            var malfunctionGroups = await _unitOfWork.MalfunctionGroupRepository.GetAllAsync();
            List<StatisticsDTO> result = new List<StatisticsDTO>();

            foreach (MalfunctionGroup group in malfunctionGroups)
            {
                result.Add(new StatisticsDTO
                {
                    FieldName = group.Name,
                    Statistics = await GetMalfunctionGroupStatistics(group.Name)
                });
            }

            return result;
        }

        public async Task<List<StatisticsDTO>> GetAllSubgroupsStatistics(string groupName = null)
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
                    Statistics = await GetMalfunctionSubGroupStatistics(subgroup.Name)
                });
            }

            return result;
        }

        public async Task<List<StatisticsDTO>> GetAllMalfunctionsStatistics(string subgroupName = null)
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
                    Statistics = await GetMalfunctionStatistics(malfunction.Name)
                });
            }

            return result;
        }
    }
}
