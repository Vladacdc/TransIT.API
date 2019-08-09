using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<int> CountMalfunction(string malfunctionName, string vehicleTypeName)
        {
            var issues =await _unitOfWork.IssueRepository.GetAllAsync(i =>i.Vehicle.VehicleType.Name == vehicleTypeName && i.Malfunction.Name==malfunctionName);
            return issues.Count();
        }

        public async Task<int> CountMalfunctionSubGroup(string malfunctionSubgroupName, string vehicleTypeName)
        {
            int count = 0;
            var malfunctions = (await _unitOfWork.MalfunctionSubgroupRepository.GetAllAsync(i => i.Name == malfunctionSubgroupName)).FirstOrDefault().Malfunction;

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
            var count = 0;
            var malfunctionSubgroups = (await _unitOfWork.MalfunctionGroupRepository.GetAllAsync(i => i.Name == malfunctionGroupName)).FirstOrDefault().MalfunctionSubgroup;

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
    }
}
