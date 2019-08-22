using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LinqKit;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;
using System;
using System.Linq.Expressions;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(TransITDBContext context)
            : base(context)
        {
        }

        public override Expression<Func<Vehicle, bool>> MakeFilteringExpression(string keyword)
        {
            return entity =>
                   entity.Brand != null && entity.Brand != string.Empty &&
                       EF.Functions.Like(entity.Brand, '%' + keyword + '%')
                || entity.RegNum != null && entity.RegNum != string.Empty &&
                       EF.Functions.Like(entity.RegNum, '%' + keyword + '%')
                || entity.InventoryId != null && entity.InventoryId != string.Empty &&
                       EF.Functions.Like(entity.InventoryId, '%' + keyword + '%')
                || entity.Model != null && entity.Model != string.Empty &&
                       EF.Functions.Like(entity.Model, '%' + keyword + '%')
                || entity.Vincode != null && entity.Vincode != string.Empty &&
                       EF.Functions.Like(entity.Vincode, '%' + keyword + '%')
                || entity.VehicleType != null && entity.VehicleType.Name != string.Empty &&
                       EF.Functions.Like(entity.VehicleType.Name, '%' + keyword + '%');
        }

        protected override IQueryable<Vehicle> ComplexEntities => Entities.
                    Include(u => u.VehicleType).
                    Include(u => u.Location).
                    Include(a => a.Create).
                    Include(b => b.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
