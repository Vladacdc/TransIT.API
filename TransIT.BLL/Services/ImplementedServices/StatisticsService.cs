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
        public async Task<int> CountMalfunction(MalfunctionDTO malfunctionDto, VehicleTypeDTO vehicleTypeDto)
        {
            var issues =await _unitOfWork.IssueRepository.GetAllAsync(i =>i.Vehicle.VehicleType.Name ==vehicleTypeDto.Name && i.Malfunction.Name==malfunctionDto.Name);
            return issues.Count();
        }

        public async Task<int> CountMalfunctionSubGroup(MalfunctionSubgroupDTO malfunctionSubgroupDto, VehicleTypeDTO vehicleTypeDto)
        {
            int count = 0;
            foreach (var i in _mapper.Map<MalfunctionSubgroup>(malfunctionSubgroupDto).Malfunction)
            {
                count +=await CountMalfunction(_mapper.Map<MalfunctionDTO>(i), vehicleTypeDto);
            }

            return count;
        }

        public async Task<int> CountMalfunctionGroup(MalfunctionGroupDTO malfunctionGroupDto, VehicleTypeDTO vehicleTypeDto)
        {
            var count = 0;
            foreach (var i in _mapper.Map<MalfunctionGroup>(malfunctionGroupDto).MalfunctionSubgroup)
            {
                count += await CountMalfunctionSubGroup(_mapper.Map<MalfunctionSubgroupDTO>(i), vehicleTypeDto);
            }

            return count;
        }
    }
}
