using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(TransITDBContext context)
            : base(context)
        {
        }

        public override Task<IQueryable<Location>> SearchExpressionAsync(IEnumerable<string> strs)
        {
            var predicate = PredicateBuilder.New<Location>();

            foreach (string keyword in strs)
            {
                string temp = keyword;
                predicate = predicate.And(entity =>
                       entity.Name != null && entity.Name != string.Empty &&
                           EF.Functions.Like(entity.Name, '%' + temp + '%')
                    || entity.Description != null && entity.Description != string.Empty &&
                           EF.Functions.Like(entity.Description, '%' + temp + '%')
                    );
            }

            return Task.FromResult(
                GetQueryable()
                .AsExpandable()
                .Where(predicate)
            );
        }

        protected override IQueryable<Location> ComplexEntities => Entities.
            Include(a => a.Create).
            Include(b => b.Mod).OrderByDescending(u => u.UpdatedDate).ThenByDescending(x => x.CreatedDate);
    }
}
