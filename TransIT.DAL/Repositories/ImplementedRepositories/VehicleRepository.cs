﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.Models;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class VehicleRepository : BaseRepository<int, Vehicle>, IVehicleRepository
    {
        public VehicleRepository(TransITDBContext context)
            : base(context)
        {
        }

        public override Task<IQueryable<Vehicle>> SearchExpressionAsync(IEnumerable<string> strs) =>
                  Task.FromResult(
                      GetQueryable().Where(entity =>
                          strs.Any(str => !string.IsNullOrEmpty(entity.Brand) && entity.Brand.ToUpperInvariant().Contains(str)
                          || !string.IsNullOrEmpty(entity.RegNum) && entity.RegNum.ToUpperInvariant().Contains(str)
                          || !string.IsNullOrEmpty(entity.InventoryId) && entity.InventoryId.ToUpperInvariant().Contains(str)
                          || !string.IsNullOrEmpty(entity.Model) && entity.Model.ToUpperInvariant().Contains(str)
                          || !string.IsNullOrEmpty(entity.Vincode) && entity.Vincode.ToUpperInvariant().Contains(str)
                          || !string.IsNullOrEmpty(entity.VehicleType.Name) && entity.VehicleType.Name.ToUpperInvariant().Contains(str)
                      )));

        protected override IQueryable<Vehicle> ComplexEntities => Entities.
                    Include(u => u.VehicleType).
                    Include(u => u.Location).
                    Include(a => a.Create).
                    Include(b => b.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
