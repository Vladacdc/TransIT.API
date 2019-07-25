using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.InterfacesRepositories;

namespace TransIT.DAL.Repositories.ImplementedRepositories
{
    public class LocationRepository : BaseRepository<Location>, ILocationRepository
    {
        public LocationRepository(DbContext context)
            : base(context)
        {
        }

        public override Task<IQueryable<Location>> SearchExpressionAsync(IEnumerable<string> strs) =>
        Task.FromResult(
                GetQueryable().Where(location =>
                    strs.Any(str => !string.IsNullOrEmpty(location.Name) && location.Name.ToUpperInvariant().Contains(str)
                                    || !string.IsNullOrEmpty(location.Description) && location.Description.ToUpperInvariant().Contains(str)))
                );

        protected override IQueryable<Location> ComplexEntities => Entities.
            Include(a => a.Create).
            Include(b => b.Mod).OrderByDescending(u => u.ModDate).ThenByDescending(x => x.CreateDate);
    }
}
