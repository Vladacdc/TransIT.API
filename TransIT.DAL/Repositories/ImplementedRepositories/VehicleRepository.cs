using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LinqKit;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(TransITDBContext context)
            : base(context)
        {
        }

        public override Task<IQueryable<Vehicle>> SearchExpressionAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<Vehicle>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
                predicate = predicate.And(entity =>
                       entity.Brand != null && entity.Brand != string.Empty &&
                           EF.Functions.Like(entity.Brand, '%' + temp + '%')
                    || entity.RegNum != null && entity.RegNum != string.Empty &&
                           EF.Functions.Like(entity.RegNum, '%' + temp + '%')
                    || entity.InventoryId != null && entity.InventoryId != string.Empty &&
                           EF.Functions.Like(entity.InventoryId, '%' + temp + '%')
                    || entity.Model != null && entity.Model != string.Empty &&
                           EF.Functions.Like(entity.Model, '%' + temp + '%')
                    || entity.Vincode != null && entity.Vincode != string.Empty &&
                           EF.Functions.Like(entity.Vincode, '%' + temp + '%')
                    || entity.VehicleType != null && entity.VehicleType.Name != string.Empty &&
                           EF.Functions.Like(entity.VehicleType.Name, '%' + temp + '%')
                    );
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }

        protected override IQueryable<Vehicle> ComplexEntities => Entities.
                    Include(u => u.VehicleType).
                    Include(u => u.Location).
                    Include(a => a.Create).
                    Include(b => b.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
