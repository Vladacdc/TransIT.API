using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;

namespace TransIT.BLL.Services.ImplementedServices
{
    public class StatisticsService : IStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="unitOfWork">Unit of work pattern</param>
        public StatisticsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> CountMalfunction(string malfunctionName, string vehicleTypeName)
        {
            var issues =await _unitOfWork.IssueRepository.GetAllAsync(i =>i.Vehicle.VehicleType.Name == vehicleTypeName && i.Malfunction.Name==malfunctionName);
            return issues.Count();
        }

        public async Task<int> CountMalfunctionSubGroup(string malfunctionSubgroupName, string vehicleTypeName)
        {
            int count = 0;
            foreach (var i in (await _unitOfWork.MalfunctionSubgroupRepository.GetAllAsync(i => i.Name == malfunctionSubgroupName)).FirstOrDefault().Malfunction)
            {
                count +=await CountMalfunction(i.Name,vehicleTypeName);
            }

            return count;
        }

        public async Task<int> CountMalfunctionGroup(string malfunctionGroupName, string vehicleTypeName)
        {
            var count = 0;
            foreach (var i in (await _unitOfWork.MalfunctionGroupRepository.GetAllAsync(i => i.Name == malfunctionGroupName)).FirstOrDefault().MalfunctionSubgroup)
            {
                count += await CountMalfunctionSubGroup(i.Name, vehicleTypeName);
            }

            return count;
        }
    }
}
